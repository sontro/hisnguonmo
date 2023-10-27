using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsApplicationRole;

namespace ACS.MANAGER.Core.AcsApplicationRole.Lock
{
    class AcsApplicationRoleChangeLockBehaviorEv : BeanObjectBase, IAcsApplicationRoleChangeLock
    {
        ACS_APPLICATION_ROLE entity;

        internal AcsApplicationRoleChangeLockBehaviorEv(CommonParam param, ACS_APPLICATION_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsApplicationRoleChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_APPLICATION_ROLE raw = new AcsApplicationRoleBO().Get<ACS_APPLICATION_ROLE>(entity.ID);
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
                    result = DAOWorker.AcsApplicationRoleDAO.Update(raw);
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
