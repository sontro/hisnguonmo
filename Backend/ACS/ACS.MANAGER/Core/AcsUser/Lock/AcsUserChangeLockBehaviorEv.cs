using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsUser;

namespace ACS.MANAGER.Core.AcsUser.Lock
{
    class AcsUserChangeLockBehaviorEv : BeanObjectBase, IAcsUserChangeLock
    {
        ACS_USER entity;

        internal AcsUserChangeLockBehaviorEv(CommonParam param, ACS_USER data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsUserChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_USER raw = new AcsUserBO().Get<ACS_USER>(entity.ID);
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
                    result = DAOWorker.AcsUserDAO.Update(raw);
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
