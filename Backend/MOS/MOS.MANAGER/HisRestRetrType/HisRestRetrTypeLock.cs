using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisRestRetrType
{
    partial class HisRestRetrTypeLock : BusinessBase
    {
        internal HisRestRetrTypeLock()
            : base()
        {

        }

        internal HisRestRetrTypeLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(long id,ref HIS_REST_RETR_TYPE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_REST_RETR_TYPE raw = null;
                valid = valid && new HisRestRetrTypeCheck().VerifyId(id, ref raw);
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
                    result = DAOWorker.HisRestRetrTypeDAO.Update(raw);
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
