using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisExpMestMetyReq
{
    partial class HisExpMestMetyReqLock : BusinessBase
    {
        internal HisExpMestMetyReqLock()
            : base()
        {

        }

        internal HisExpMestMetyReqLock(CommonParam paramLock)
            : base(paramLock)
        {

        }
		
		internal bool Lock(long id, ref HIS_EXP_MEST_METY_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_EXP_MEST_METY_REQ data = new HisExpMestMetyReqGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        result = DAOWorker.HisExpMestMetyReqDAO.Update(data);
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
		
		internal bool Unlock(long id, ref HIS_EXP_MEST_METY_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_EXP_MEST_METY_REQ data = new HisExpMestMetyReqGet().GetById(id);
                    if (data != null)
                    {
                        data.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        result = DAOWorker.HisExpMestMetyReqDAO.Update(data);
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

        internal bool ChangeLock(long id, ref HIS_EXP_MEST_METY_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST_METY_REQ raw = null;
                valid = valid && new HisExpMestMetyReqCheck().VerifyId(id, ref raw);
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
                    result = DAOWorker.HisExpMestMetyReqDAO.Update(raw);
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
