using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisService
{
    class HisServiceLock : BusinessBase
    {
        internal HisServiceLock()
            : base()
        {

        }

        internal HisServiceLock(Inventec.Core.CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(long id, ref HIS_SERVICE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERVICE raw = null;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_SERVICE data = new HisServiceGet().GetById(id);
                    if (data != null)
                    {
                        valid = valid && new HisServiceCheck().VerifyId(data.ID, ref raw);
                        if (data.IS_ACTIVE.HasValue && data.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                            this.EventLogLock(raw);
                        }
                        else
                        {
                            data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                            this.EventLogUnLock(raw);
                        }
                        result = DAOWorker.HisServiceDAO.Update(data);
                        if (result) resultData = data;
                        
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

        internal bool ChangeLock(long id, short? isActive)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_SERVICE data = new HisServiceGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = isActive;
                        result = DAOWorker.HisServiceDAO.Update(data);
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

        private void EventLogLock(HIS_SERVICE dataLock)
        {
            try
            {
                List<string> logs = new List<string>();
                logs.Add(String.Format("{0} ==> {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MoKhoa), LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khoa)));

                new EventLogGenerator(EventLog.Enum.HisService_KhoaDanhMucKyThuat, String.Join(". ", logs))
                        .ServiceCode(dataLock.SERVICE_CODE)
                        .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void EventLogUnLock(HIS_SERVICE dataUnLock)
        {
            try
            {
                List<string> logs = new List<string>();
                logs.Add(String.Format("{0} ==> {1}", LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.Khoa), LogCommonUtil.GetEventLogContent(LibraryEventLog.EventLog.Enum.MoKhoa)));

                new EventLogGenerator(EventLog.Enum.HisService_MoKhoaDanhMucKyThuat, String.Join(". ", logs))
                        .ServiceCode(dataUnLock.SERVICE_CODE)
                        .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
