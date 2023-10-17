using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisService;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisMaterial
{
    class HisMaterialLock : BusinessBase
    {
        internal HisMaterialLock()
            : base()
        {

        }

        internal HisMaterialLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool Lock(HisMaterialChangeLockSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_MEDI_STOCK mediStock = null;
                HIS_MATERIAL dataLock = null;
                HisMediStockCheck stockChecker = new HisMediStockCheck(param);
                HisMaterialCheck materialChecker = new HisMaterialCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && materialChecker.VerifyId(data.MaterialId, ref dataLock);
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
                    valid = valid && materialChecker.IsUnLock(dataLock);
                }
                if (valid)
                {
                    List<string> lstSql = new List<string>();

                    decimal beanAmount = 0;
                    string lockingReason = String.IsNullOrEmpty(data.LockingReason) ? "NULL" : string.Format("\'{0}\'", data.LockingReason);
                    if (mediStock != null)
                    {
                        beanAmount = DAOWorker.SqlDAO.GetSqlSingle<decimal>("SELECT NVL(SUM(AMOUNT), 0) FROM HIS_MATERIAL_BEAN WHERE MATERIAL_ID = :param1 AND MEDI_STOCK_ID = :param2 AND IS_ACTIVE = 1", dataLock.ID, mediStock.ID);
                        lstSql.Add(string.Format("UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 0, LOCKING_REASON = {0} WHERE MATERIAL_ID = {1} AND MEDI_STOCK_ID = {2} AND IS_ACTIVE = 1", lockingReason, dataLock.ID, mediStock.ID));
                        if (beanAmount <= 0)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMaterial_LoVatTuKhongConKhaDungTrongKho);
                            return false;
                        }
                    }
                    else
                    {
                        lstSql.Add(string.Format("UPDATE HIS_MATERIAL SET IS_ACTIVE = 0, LOCKING_REASON = {0} WHERE ID = {1}", lockingReason, dataLock.ID));
                        lstSql.Add(string.Format("UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 0, LOCKING_REASON = {0} WHERE MATERIAL_ID = {1} AND MEDI_STOCK_ID IS NOT NULL AND IS_ACTIVE = 1", lockingReason, dataLock.ID));
                    }

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

        private void EventLogLock(HIS_MATERIAL dataLock, HIS_MEDI_STOCK mediStock, decimal beanAmount, HisMaterialChangeLockSDO data)
        {
            try
            {
                HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == dataLock.MATERIAL_TYPE_ID);

                List<string> logs = new List<string>();
                string materialTypeCode = materialType != null ? materialType.MATERIAL_TYPE_CODE : "";
                string materialTypeName = materialType != null ? materialType.MATERIAL_TYPE_NAME : "";

                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ten), materialTypeName));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo), dataLock.PACKAGE_NUMBER));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDo), data.LockingReason));

                if (mediStock != null)
                {
                    logs.Insert(0, String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Kho), mediStock.MEDI_STOCK_NAME));
                    logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong), beanAmount));
                    new EventLogGenerator(EventLog.Enum.HisMaterial_KhoaLoVatTuTrongKho, String.Join(", ", logs))
                        .MaterialTypeCode(materialTypeCode).Run();
                }
                else
                {
                    new EventLogGenerator(EventLog.Enum.HisMaterial_KhoaLoVatTu, String.Join(", ", logs))
                        .MaterialTypeCode(materialTypeCode).Run();
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        internal bool Unlock(HisMaterialChangeLockSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                WorkPlaceSDO workPlace = null;
                HIS_MEDI_STOCK mediStock = null;
                HIS_MATERIAL dataUnlock = null;
                HisMediStockCheck stockChecker = new HisMediStockCheck(param);
                HisMaterialCheck materialChecker = new HisMaterialCheck(param);

                valid = valid && IsNotNull(data);
                valid = valid && materialChecker.VerifyId(data.MaterialId, ref dataUnlock);
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
                    valid = valid && materialChecker.IsLock(dataUnlock);
                }

                if (valid)
                {
                    List<string> lstSql = new List<string>();

                    decimal beanAmount = 0;

                    string lockingReason = String.IsNullOrEmpty(data.LockingReason) ? "NULL" : string.Format("\'{0}\'", data.LockingReason);
                    if (mediStock != null)
                    {
                        beanAmount = DAOWorker.SqlDAO.GetSqlSingle<decimal>("SELECT NVL(SUM(AMOUNT), 0) FROM HIS_MATERIAL_BEAN WHERE MATERIAL_ID = :param1 AND MEDI_STOCK_ID = :param2 AND EXP_MEST_MATERIAL_ID IS NULL AND SESSION_KEY IS NULL", dataUnlock.ID, mediStock.ID);
                        lstSql.Add(string.Format("UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, LOCKING_REASON = {0} WHERE MATERIAL_ID = {1} AND MEDI_STOCK_ID = {2} AND EXP_MEST_MATERIAL_ID IS NULL AND SESSION_KEY IS NULL", lockingReason, dataUnlock.ID, mediStock.ID));
                        if (beanAmount <= 0)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMaterial_LoVatTuTrongKhoDaThuocPhieuXuatHoacDangThucHienKeXuat);
                            return false;
                        }
                        if (dataUnlock.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMaterial_LoVatTuDangBiKhoa);
                            return false;
                        }
                    }
                    else
                    {
                        lstSql.Add(string.Format("UPDATE HIS_MATERIAL SET IS_ACTIVE = 1, LOCKING_REASON = {0} WHERE ID = {1}", lockingReason, dataUnlock.ID));
                        lstSql.Add(string.Format("UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE = 1, LOCKING_REASON = {0} WHERE MATERIAL_ID = {1} AND MEDI_STOCK_ID IS NOT NULL AND EXP_MEST_MATERIAL_ID IS NULL AND SESSION_KEY IS NULL", lockingReason, dataUnlock.ID));
                    }

                    if (!DAOWorker.SqlDAO.Execute(lstSql))
                    {
                        return false;
                    }

                    this.EventLogUnlock(dataUnlock, mediStock, beanAmount, data);

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

        private void EventLogUnlock(HIS_MATERIAL dataUnlock, HIS_MEDI_STOCK mediStock, decimal beanAmount, HisMaterialChangeLockSDO data)
        {
            try
            {
                HIS_MATERIAL_TYPE materialType = HisMaterialTypeCFG.DATA.FirstOrDefault(o => o.ID == dataUnlock.MATERIAL_TYPE_ID);

                List<string> logs = new List<string>();
                string materialTypeCode = materialType != null ? materialType.MATERIAL_TYPE_CODE : "";
                string materialTypeName = materialType != null ? materialType.MATERIAL_TYPE_NAME : "";

                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Ten), materialTypeName));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLo), dataUnlock.PACKAGE_NUMBER));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDo), data.LockingReason));

                if (mediStock != null)
                {
                    logs.Insert(0, String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Kho), mediStock.MEDI_STOCK_NAME));
                    logs.Add(String.Format("{0} {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.SoLuong), beanAmount));
                    new EventLogGenerator(EventLog.Enum.HisMaterial_MoKhoaLoVatTuTrongKho, String.Join(", ", logs))
                        .MaterialTypeCode(materialTypeCode)
                        .Run();
                }
                else
                {
                    new EventLogGenerator(EventLog.Enum.HisMaterial_MoKhoaLoVatTu, String.Join(", ", logs))
                        .MaterialTypeCode(materialTypeCode)
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
                    HIS_MATERIAL data = new HisMaterialGet().GetById(id);
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
                        result = DAOWorker.HisMaterialDAO.Update(data);
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
