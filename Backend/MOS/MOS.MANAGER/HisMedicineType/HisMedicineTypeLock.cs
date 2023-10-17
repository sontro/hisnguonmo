using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMedicineType
{
    partial class HisMedicineTypeLock : BusinessBase
    {
        internal HisMedicineTypeLock()
            : base()
        {

        }

        internal HisMedicineTypeLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool Lock(HIS_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDICINE_TYPE raw = null;
                valid = valid && new HisMedicineTypeCheck().VerifyId(data.ID, ref raw);
                if (String.IsNullOrWhiteSpace(data.LOCKING_REASON))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    LogSystem.Warn("Thieu thong tin ly do khoa");
                    valid = false;
                }
                if (valid && raw != null)
                {
                    if (!raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                        throw new Exception("Du lieu dang bi khoa");
                    }
                    raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                    raw.LOCKING_REASON = data.LOCKING_REASON;

                    result = DAOWorker.HisMedicineTypeDAO.Update(raw);
                    if (result)
                    {
                        new HisServiceLock().ChangeLock(raw.SERVICE_ID, raw.IS_ACTIVE);
                        data.IS_ACTIVE = raw.IS_ACTIVE;
                    }
                }
                else
                {
                    BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                }
                this.EventLogLock(raw);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }


        internal bool Unlock(HIS_MEDICINE_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDICINE_TYPE raw = null;

                valid = valid && new HisMedicineTypeCheck().VerifyId(data.ID, ref raw);
                if (valid && raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDaMoKhoa);
                        throw new Exception("Du lieu dang mo khoa");
                    }

                    raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                    raw.LOCKING_REASON = null;
                    result = DAOWorker.HisMedicineTypeDAO.Update(raw);
                    if (result)
                    {
                        new HisServiceLock().ChangeLock(raw.SERVICE_ID, raw.IS_ACTIVE);
                        data.IS_ACTIVE = raw.IS_ACTIVE;
                    }
                }
                else
                {
                    BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                }
                this.EventLogUnLock(raw);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void EventLogLock(HIS_MEDICINE_TYPE dataLock)
        {
            try
            {
                List<string> logs = new List<string>();
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenThuoc), dataLock.MEDICINE_TYPE_NAME));
                logs.Add(String.Format("{0} ==> {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MoKhoa), LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khoa)));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDo), dataLock.LOCKING_REASON));

                new EventLogGenerator(EventLog.Enum.HisMedicineType_KhoaLoaiThuoc, String.Join(". ", logs))
                        .MedicineTypeId(dataLock.ID.ToString())
                        .MedicineTypeCode(dataLock.MEDICINE_TYPE_CODE)
                        .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void EventLogUnLock(HIS_MEDICINE_TYPE dataUnLock)
        {
            try
            {
                List<string> logs = new List<string>();
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenThuoc), dataUnLock.MEDICINE_TYPE_NAME));
                logs.Add(String.Format("{0} ==> {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khoa), LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MoKhoa)));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDo), dataUnLock.LOCKING_REASON));

                new EventLogGenerator(EventLog.Enum.HisMedicineType_MoKhoaLoaiThuoc, String.Join(". ", logs))
                        .MedicineTypeId(dataUnLock.ID.ToString())
                        .MedicineTypeCode(dataUnLock.MEDICINE_TYPE_CODE)
                        .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
