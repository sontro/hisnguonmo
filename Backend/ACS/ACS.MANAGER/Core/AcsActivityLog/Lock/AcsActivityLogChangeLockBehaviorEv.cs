using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsActivityLog;

namespace ACS.MANAGER.Core.AcsActivityLog.Lock
{
    class AcsActivityLogChangeLockBehaviorEv : BeanObjectBase, IAcsActivityLogChangeLock
    {
        ACS_ACTIVITY_LOG entity;

        internal AcsActivityLogChangeLockBehaviorEv(CommonParam param, ACS_ACTIVITY_LOG data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsActivityLogChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_ACTIVITY_LOG raw = new AcsActivityLogBO().Get<ACS_ACTIVITY_LOG>(entity.ID);
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
                    result = DAOWorker.AcsActivityLogDAO.Update(raw);
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
