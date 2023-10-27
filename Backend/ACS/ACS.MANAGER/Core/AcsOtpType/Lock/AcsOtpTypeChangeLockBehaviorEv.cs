using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsOtpType;

namespace ACS.MANAGER.Core.AcsOtpType.Lock
{
    class AcsOtpTypeChangeLockBehaviorEv : BeanObjectBase, IAcsOtpTypeChangeLock
    {
        ACS_OTP_TYPE entity;

        internal AcsOtpTypeChangeLockBehaviorEv(CommonParam param, ACS_OTP_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsOtpTypeChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_OTP_TYPE raw = new AcsOtpTypeBO().Get<ACS_OTP_TYPE>(entity.ID);
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
                    result = DAOWorker.AcsOtpTypeDAO.Update(raw);
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
