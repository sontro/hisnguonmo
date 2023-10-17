using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisBedLog
{
    partial class HisBedLogLock : BusinessBase
    {
        internal HisBedLogLock()
            : base()
        {

        }

        internal HisBedLogLock(CommonParam paramLock)
            : base(paramLock)
        {

        }
		
		internal bool Lock(long id, ref HIS_BED_LOG resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_BED_LOG data = new HisBedLogGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        result = DAOWorker.HisBedLogDAO.Update(data);
                        resultData = result ? data : null;
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
		
		internal bool Unlock(long id, ref HIS_BED_LOG resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_BED_LOG data = new HisBedLogGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        result = DAOWorker.HisBedLogDAO.Update(data);
                        resultData = result ? data : null;
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

        internal bool ChangeLock(HIS_BED_LOG data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_BED_LOG raw = null;
                valid = valid && new HisBedLogCheck().VerifyId(data.ID, ref raw);
                if (valid && raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.HisBedLogDAO.Update(raw);
                    if (result) data.IS_ACTIVE = raw.IS_ACTIVE;
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
    }
}
