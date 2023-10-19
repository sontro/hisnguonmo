using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsControl;

namespace ACS.MANAGER.Core.AcsControl.Lock
{
    class AcsControlChangeLockBehaviorEv : BeanObjectBase, IAcsControlChangeLock
    {
        ACS_CONTROL entity;

        internal AcsControlChangeLockBehaviorEv(CommonParam param, ACS_CONTROL data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsControlChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_CONTROL raw = new AcsControlBO().Get<ACS_CONTROL>(entity.ID);
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
                    result = DAOWorker.AcsControlDAO.Update(raw);
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
