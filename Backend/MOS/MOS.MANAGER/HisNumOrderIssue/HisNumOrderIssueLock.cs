using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisNumOrderIssue
{
    partial class HisNumOrderIssueLock : BusinessBase
    {
        internal HisNumOrderIssueLock()
            : base()
        {

        }

        internal HisNumOrderIssueLock(CommonParam paramLock)
            : base(paramLock)
        {

        }
		
		internal bool Lock(long id, ref HIS_NUM_ORDER_ISSUE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_NUM_ORDER_ISSUE data = new HisNumOrderIssueGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        result = DAOWorker.HisNumOrderIssueDAO.Update(data);
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
		
		internal bool Unlock(long id, ref HIS_NUM_ORDER_ISSUE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_NUM_ORDER_ISSUE data = new HisNumOrderIssueGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        result = DAOWorker.HisNumOrderIssueDAO.Update(data);
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

        internal bool ChangeLock(long id, ref HIS_NUM_ORDER_ISSUE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_NUM_ORDER_ISSUE raw = null;
                valid = valid && new HisNumOrderIssueCheck().VerifyId(id, ref raw);
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
                    result = DAOWorker.HisNumOrderIssueDAO.Update(raw);
                    if (result) resultData = raw;
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
