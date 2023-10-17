using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisImpMestPay
{
    partial class HisImpMestPayLock : BusinessBase
    {
        internal HisImpMestPayLock()
            : base()
        {

        }

        internal HisImpMestPayLock(CommonParam paramLock)
            : base(paramLock)
        {

        }
		
		internal bool Lock(long id, ref HIS_IMP_MEST_PAY resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_IMP_MEST_PAY data = new HisImpMestPayGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        result = DAOWorker.HisImpMestPayDAO.Update(data);
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
		
		internal bool Unlock(long id, ref HIS_IMP_MEST_PAY resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_IMP_MEST_PAY data = new HisImpMestPayGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        result = DAOWorker.HisImpMestPayDAO.Update(data);
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

        internal bool ChangeLock(long id, ref HIS_IMP_MEST_PAY resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST_PAY raw = null;
                valid = valid && new HisImpMestPayCheck().VerifyId(id, ref raw);
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
                    result = DAOWorker.HisImpMestPayDAO.Update(raw);
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
