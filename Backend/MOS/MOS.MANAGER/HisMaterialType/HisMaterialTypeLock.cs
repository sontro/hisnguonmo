using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisService;
using MOS.UTILITY;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMaterialType
{
    partial class HisMaterialTypeLock : BusinessBase
    {
        internal HisMaterialTypeLock()
            : base()
        {

        }

        internal HisMaterialTypeLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool Lock(HIS_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MATERIAL_TYPE raw = null;
                valid = valid && new HisMaterialTypeCheck().VerifyId(data.ID, ref raw);
                if (String.IsNullOrWhiteSpace(data.LOCKING_REASON))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                    LogSystem.Warn("Thieu thong tin ly do khoa");
                    valid = false;
                }
                if (valid && raw != null)
                {
                    if (!raw.IS_ACTIVE.HasValue || raw.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                        throw new Exception("Du lieu dang bi khoa");
                    }

                    raw.LOCKING_REASON = data.LOCKING_REASON;
                    raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;

                    result = DAOWorker.HisMaterialTypeDAO.Update(raw);
                    if (result)
                    {
                        new HisServiceLock().ChangeLock(raw.SERVICE_ID, raw.IS_ACTIVE);
                        data.IS_ACTIVE = raw.IS_ACTIVE;
                    }
                    this.EventLogLock(raw);
                }
                else
                {
                    BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
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

        internal bool Unlock(HIS_MATERIAL_TYPE data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MATERIAL_TYPE raw = null;
                valid = valid && new HisMaterialTypeCheck().VerifyId(data.ID, ref raw);
                if (valid && raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDaMoKhoa);
                        throw new Exception("Du lieu dang mo khoa");
                    }

                    raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                    raw.LOCKING_REASON = null;

                    result = DAOWorker.HisMaterialTypeDAO.Update(raw);
                    if (result)
                    {
                        new HisServiceLock().ChangeLock(raw.SERVICE_ID, raw.IS_ACTIVE);
                        data.IS_ACTIVE = raw.IS_ACTIVE;
                    }
                    this.EventLogUnLock(raw);
                }
                else
                {
                    BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
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

        private void EventLogLock(HIS_MATERIAL_TYPE dataLock)
        {
            try
            {
                List<string> logs = new List<string>();
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenVatTu), dataLock.MATERIAL_TYPE_NAME));
                logs.Add(String.Format("{0} ==> {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MoKhoa), LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khoa)));
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDo), dataLock.LOCKING_REASON));

                new EventLogGenerator(EventLog.Enum.HisMaterialType_KhoaLoaiVatTu, String.Join(". ", logs))
                        .MaterialTypeId(dataLock.ID.ToString())
                        .MaterialTypeCode(dataLock.MATERIAL_TYPE_CODE)
                        .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void EventLogUnLock(HIS_MATERIAL_TYPE dataUnLock)
        {
            try
            {
                List<string> logs = new List<string>();
                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.TenVatTu), dataUnLock.MATERIAL_TYPE_NAME));
                logs.Add(String.Format("{0} ==> {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khoa), LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MoKhoa)));

                logs.Add(String.Format("{0}: {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.LyDo), dataUnLock.LOCKING_REASON));

                new EventLogGenerator(EventLog.Enum.HisMaterialType_MoKhoaLoaiVatTu, String.Join(". ", logs))
                        .MaterialTypeId(dataUnLock.ID.ToString())
                        .MaterialTypeCode(dataUnLock.MATERIAL_TYPE_CODE)
                        .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
