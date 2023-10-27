using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsControlRole;

namespace ACS.MANAGER.Core.AcsControlRole.Lock
{
    class AcsControlRoleChangeLockBehaviorEv : BeanObjectBase, IAcsControlRoleChangeLock
    {
        ACS_CONTROL_ROLE entity;

        internal AcsControlRoleChangeLockBehaviorEv(CommonParam param, ACS_CONTROL_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsControlRoleChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_CONTROL_ROLE raw = new AcsControlRoleBO().Get<ACS_CONTROL_ROLE>(entity.ID);
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
                    result = DAOWorker.AcsControlRoleDAO.Update(raw);
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
