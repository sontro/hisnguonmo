using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsActivityType;

namespace ACS.MANAGER.Core.AcsActivityType.Lock
{
    class AcsActivityTypeChangeLockBehaviorEv : BeanObjectBase, IAcsActivityTypeChangeLock
    {
        ACS_ACTIVITY_TYPE entity;

        internal AcsActivityTypeChangeLockBehaviorEv(CommonParam param, ACS_ACTIVITY_TYPE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsActivityTypeChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_ACTIVITY_TYPE raw = new AcsActivityTypeBO().Get<ACS_ACTIVITY_TYPE>(entity.ID);
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
                    result = DAOWorker.AcsActivityTypeDAO.Update(raw);
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
