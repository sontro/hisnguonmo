using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsRole;

namespace ACS.MANAGER.Core.AcsRole.Lock
{
    class AcsRoleChangeLockBehaviorEv : BeanObjectBase, IAcsRoleChangeLock
    {
        ACS_ROLE entity;

        internal AcsRoleChangeLockBehaviorEv(CommonParam param, ACS_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_ROLE raw = new AcsRoleBO().Get<ACS_ROLE>(entity.ID);
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
                    result = DAOWorker.AcsRoleDAO.Update(raw);
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
