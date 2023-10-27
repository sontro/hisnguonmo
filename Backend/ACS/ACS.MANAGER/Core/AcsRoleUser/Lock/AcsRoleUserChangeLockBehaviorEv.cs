using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsRoleUser;

namespace ACS.MANAGER.Core.AcsRoleUser.Lock
{
    class AcsRoleUserChangeLockBehaviorEv : BeanObjectBase, IAcsRoleUserChangeLock
    {
        ACS_ROLE_USER entity;

        internal AcsRoleUserChangeLockBehaviorEv(CommonParam param, ACS_ROLE_USER data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleUserChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_ROLE_USER raw = new AcsRoleUserBO().Get<ACS_ROLE_USER>(entity.ID);
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
                    result = DAOWorker.AcsRoleUserDAO.Update(raw);
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
