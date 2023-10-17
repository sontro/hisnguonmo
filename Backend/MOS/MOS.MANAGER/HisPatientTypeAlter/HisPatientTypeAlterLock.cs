using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisPatientTypeAlter
{
    class HisPatientTypeAlterLock : BusinessBase
    {
        internal HisPatientTypeAlterLock()
            : base()
        {

        }

        internal HisPatientTypeAlterLock(CommonParam paramLock)
            : base(paramLock)
        {

        }

        internal bool ChangeLock(HIS_PATIENT_TYPE_ALTER data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(data.ID);
                if (valid)
                {
                    data = new HisPatientTypeAlterGet().GetById(data.ID);
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
                        result = DAOWorker.HisPatientTypeAlterDAO.Update(data);
                    }
                    else
                    {
                        BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
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
