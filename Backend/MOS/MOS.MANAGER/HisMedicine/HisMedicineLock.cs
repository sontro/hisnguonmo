using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMedicine
{
    class HisMedicineLock : BusinessBase
    {
        internal HisMedicineLock()
            : base()
        {

        }

        internal HisMedicineLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool Lock(HisMedicineChangeLockSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_MEDI_STOCK mediStock = null;
                HIS_MEDICINE dataLock = null;
                HisMediStockCheck stockChecker = new HisMediStockCheck(param);
                HisMedicineCheck medicineChecker = new HisMedicineCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && medicineChecker.VerifyId(data.MedicineId, ref dataLock);
                //chon kho thi phai co phong lam viec
                //khong chon kho khoa tat ca
                if (data.MediStockId.HasValue)
                {
                    valid = valid && stockChecker.VerifyId(data.MediStockId.Value, ref mediStock);
                    valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId ?? 0, ref workPlace);
                    valid = valid && this.IsWorkingAtRoom(mediStock.ROOM_ID, workPlace.RoomId);
                }
                else
                {
                    valid = valid && medicineChecker.IsUnLock(dataLock);
                }
                if (valid)
                {
                    List<string> lstSql = new List<string>();

                    decimal beanAmount = 0;

                    string lockingReason = String.IsNullOrEmpty(data.LockingReason) ? "NULL" : string.Format("\'{0}\'", data.LockingReason);
                    if (mediStock != null)
                    {
                        beanAmount = DAOWorker.SqlDAO.GetSqlSingle<decimal>("SELECT NVL(SUM(AMOUNT), 0) FROM HIS_MEDICINE_BEAN WHERE MEDICINE_ID = :param1 AND MEDI_STOCK_ID = :param2 AND IS_ACTIVE = 1", dataLock.ID, mediStock.ID);
                        if (beanAmount <= 0)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicine_LoThuocKhongConKhaDungTrongKho);
                            return false;
                        }
                        lstSql.Add(string.Format("UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 0, LOCKING_REASON = {0} WHERE MEDICINE_ID = {1} AND MEDI_STOCK_ID = {2} AND IS_ACTIVE = 1", lockingReason, dataLock.ID, mediStock.ID));
                    }
                    else
                    {
                        lstSql.Add(string.Format("UPDATE HIS_MEDICINE SET IS_ACTIVE = 0, LOCKING_REASON = {0} WHERE ID = {1}", lockingReason, dataLock.ID));
                        lstSql.Add(string.Format("UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 0, LOCKING_REASON = {0} WHERE MEDICINE_ID = {1} AND MEDI_STOCK_ID IS NOT NULL AND IS_ACTIVE = 1", lockingReason, dataLock.ID));
                    }
                    //dataLock.LOCKING_REASON = data.LockingReason;

                    if (!DAOWorker.SqlDAO.Execute(lstSql))
                    {
                        return false;
                    }
                    

                    this.EventLogLock(dataLock, mediStock, beanAmount, data);
                    
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void EventLogLock(HIS_MEDICINE dataLock, HIS_MEDI_STOCK mediStock, decimal beanAmount, HisMedicineChangeLockSDO data)
        {
            try
            {
                HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == dataLock.MEDICINE_TYPE_ID);

                List<string> logs = new List<string>();
                string medicineTypeCode = medicineType != null ? medicineType.MEDICINE_TYPE_CODE : "";
                string medicineTypeName = medicineType != null ? medicineType.MEDICINE_TYPE_NAME : "";

                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ten), medicineTypeName));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo), dataLock.PACKAGE_NUMBER));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDo), data.LockingReason));

                if (mediStock != null)
                {
                    logs.Insert(0, String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Kho), mediStock.MEDI_STOCK_NAME));
                    logs.Add(String.Format("{0} {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong), beanAmount));
                    new EventLogGenerator(EventLog.Enum.HisMedicine_KhoaLoThuocTrongKho, String.Join(", ", logs))
                        .MedicineTypeCode(medicineTypeCode)
                        .Run();
                }
                else
                {
                    new EventLogGenerator(EventLog.Enum.HisMedicine_KhoaLoThuoc, String.Join(", ", logs))
                        .MedicineTypeCode(medicineTypeCode)
                        .Run();
                }


            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
       
        internal bool Unlock(HisMedicineChangeLockSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_MEDI_STOCK mediStock = null;
                HIS_MEDICINE dataUnlock = null;
                HisMediStockCheck stockChecker = new HisMediStockCheck(param);
                HisMedicineCheck medicineChecker = new HisMedicineCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && medicineChecker.VerifyId(data.MedicineId, ref dataUnlock);
                //chon kho thi phai co phong lam viec
                //khong chon kho mo khoa tat ca
                if (data.MediStockId.HasValue)
                {
                    valid = valid && stockChecker.VerifyId(data.MediStockId.Value, ref mediStock);
                    valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId ?? 0, ref workPlace);
                    valid = valid && this.IsWorkingAtRoom(mediStock.ROOM_ID, workPlace.RoomId);
                }
                else
                {
                    valid = valid && medicineChecker.IsLock(dataUnlock);
                }

                if (valid)
                {
                    List<string> lstSql = new List<string>();

                    decimal beanAmount = 0;
                    string lockingReason = String.IsNullOrEmpty(data.LockingReason) ? "NULL" : string.Format("\'{0}\'", data.LockingReason);
                    if (mediStock != null)
                    {
                        beanAmount = DAOWorker.SqlDAO.GetSqlSingle<decimal>("SELECT NVL(SUM(AMOUNT), 0) FROM HIS_MEDICINE_BEAN WHERE MEDICINE_ID = :param1 AND MEDI_STOCK_ID = :param2 AND EXP_MEST_MEDICINE_ID IS NULL AND SESSION_KEY IS NULL", dataUnlock.ID, mediStock.ID);
                        lstSql.Add(string.Format("UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, LOCKING_REASON = {0} WHERE MEDICINE_ID = {1} AND MEDI_STOCK_ID = {2} AND EXP_MEST_MEDICINE_ID IS NULL AND SESSION_KEY IS NULL", lockingReason, dataUnlock.ID, mediStock.ID));
                        if (beanAmount <= 0)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicine_LoThuocTrongKhoDaThuocPhieuXuatHoacDangThucHienKeXuat);
                            return false;
                        }

                        if (dataUnlock.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicine_LoThuocDangBiKhoa);
                            return false;
                        }
                    }
                    else
                    {
                        lstSql.Add(string.Format("UPDATE HIS_MEDICINE SET IS_ACTIVE = 1, LOCKING_REASON = {0} WHERE ID = {1}", lockingReason, dataUnlock.ID));
                        lstSql.Add(string.Format("UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, LOCKING_REASON = {0} WHERE MEDICINE_ID = {1} AND MEDI_STOCK_ID IS NOT NULL AND EXP_MEST_MEDICINE_ID IS NULL AND SESSION_KEY IS NULL", lockingReason, dataUnlock.ID));
                    }

                    if (!DAOWorker.SqlDAO.Execute(lstSql))
                    {
                        return false;
                    }

                    string eventLog = "";
                    this.EventLogUnlock(dataUnlock, mediStock, beanAmount, ref eventLog, data);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void EventLogUnlock(HIS_MEDICINE datUnlock, HIS_MEDI_STOCK mediStock, decimal beanAmount, ref string eventLog, HisMedicineChangeLockSDO data)
        {
            try
            {
                HIS_MEDICINE_TYPE medicineType = HisMedicineTypeCFG.DATA.FirstOrDefault(o => o.ID == datUnlock.MEDICINE_TYPE_ID);

                List<string> editFields = new List<string>();

                List<string> logs = new List<string>();
                string medicineTypeCode = medicineType != null ? medicineType.MEDICINE_TYPE_CODE : "";
                string medicineTypeName = medicineType != null ? medicineType.MEDICINE_TYPE_NAME : "";

                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ten), medicineTypeName));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo), datUnlock.PACKAGE_NUMBER));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDo), data.LockingReason));

                if (mediStock != null)
                {
                    logs.Insert(0, String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Kho), mediStock.MEDI_STOCK_NAME));
                    logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong), beanAmount));
                    new EventLogGenerator(EventLog.Enum.HisMedicine_MoKhoaLoThuocTrongKho, String.Join(", ", logs))
                        .MedicineTypeCode(medicineTypeCode)
                        .Run();
                }
                else
                {
                    new EventLogGenerator(EventLog.Enum.HisMedicine_MoKhoaLoThuoc, String.Join(", ", logs))
                        .MedicineTypeCode(medicineTypeCode)
                        .Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal bool ChangeLock(long id)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_MEDICINE data = new HisMedicineGet().GetById(id);
                    if (data != null)
                    {
                        if (data.IS_ACTIVE.HasValue && data.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        }
                        else
                        {
                            data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        }
                        result = DAOWorker.HisMedicineDAO.Update(data);
                    }
                    else
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
