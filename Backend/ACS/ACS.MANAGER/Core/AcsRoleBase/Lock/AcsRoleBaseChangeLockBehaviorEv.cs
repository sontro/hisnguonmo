using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsRoleBase;

namespace ACS.MANAGER.Core.AcsRoleBase.Lock
{
    class AcsRoleBaseChangeLockBehaviorEv : BeanObjectBase, IAcsRoleBaseChangeLock
    {
        ACS_ROLE_BASE entity;

        internal AcsRoleBaseChangeLockBehaviorEv(CommonParam param, ACS_ROLE_BASE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleBaseChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_ROLE_BASE raw = new AcsRoleBaseBO().Get<ACS_ROLE_BASE>(entity.ID);
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
                    result = DAOWorker.AcsRoleBaseDAO.Update(raw);
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
