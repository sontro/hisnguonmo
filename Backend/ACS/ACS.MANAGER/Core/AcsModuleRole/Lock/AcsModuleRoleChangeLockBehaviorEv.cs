using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsModuleRole;

namespace ACS.MANAGER.Core.AcsModuleRole.Lock
{
    class AcsModuleRoleChangeLockBehaviorEv : BeanObjectBase, IAcsModuleRoleChangeLock
    {
        ACS_MODULE_ROLE entity;

        internal AcsModuleRoleChangeLockBehaviorEv(CommonParam param, ACS_MODULE_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleRoleChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_MODULE_ROLE raw = new AcsModuleRoleBO().Get<ACS_MODULE_ROLE>(entity.ID);
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
                    result = DAOWorker.AcsModuleRoleDAO.Update(raw);
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
