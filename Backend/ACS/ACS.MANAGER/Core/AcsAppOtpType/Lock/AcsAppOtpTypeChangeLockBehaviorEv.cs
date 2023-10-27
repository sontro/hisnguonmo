using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsAppOtpType;

namespace ACS.MANAGER.Core.AcsAppOtpType.Lock
{
    class AcsAppOtpTypeChangeLockBehaviorEv : BeanObjectBase, IAcsAppOtpTypeChangeLock
    {
        ACS_APP_OTP_TYPE entity;

        internal AcsAppOtpTypeChangeLockBehaviorEv(CommonParam param, ACS_APP_OTP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsAppOtpTypeChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_APP_OTP_TYPE raw = new AcsAppOtpTypeBO().Get<ACS_APP_OTP_TYPE>(entity.ID);
                if (raw != null)
                {
                    if (raw.IS_ACTIVE.HasValue && raw.IS_ACTIVE == IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE)
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__FALSE;
                    }
                    else
                    {
                        raw.IS_ACTIVE = IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE;
                    }
                    result = DAOWorker.AcsAppOtpTypeDAO.Update(raw);
                    if (result) entity.IS_ACTIVE = raw.IS_ACTIVE;
                }
                else
                {
                    BugUtil.SetBugCode(param, ACS.LibraryBug.Bug.Enum.Common__KXDDDuLieuCanXuLy);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
