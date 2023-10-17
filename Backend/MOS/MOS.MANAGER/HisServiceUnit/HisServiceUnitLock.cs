using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisServiceUnit
{
    class HisServiceUnitLock : BusinessBase
    {
        internal HisServiceUnitLock()
            : base()
        {

        }

        internal HisServiceUnitLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(long id, ref HIS_SERVICE_UNIT resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    HIS_SERVICE_UNIT raw = new HisServiceUnitGet().GetById(id);
                    if (raw != null)
                    {
                        if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                        {
                            raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE;
                        }
                        else
                        {
                            raw.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
                        }
                        result = DAOWorker.HisServiceUnitDAO.Update(raw);
                        if (result)
                        {
                            resultData = raw;
                        }
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
