using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsApplication;

namespace ACS.MANAGER.Core.AcsApplication.Lock
{
    class AcsApplicationChangeLockBehaviorEv : BeanObjectBase, IAcsApplicationChangeLock
    {
        ACS_APPLICATION entity;

        internal AcsApplicationChangeLockBehaviorEv(CommonParam param, ACS_APPLICATION data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsApplicationChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_APPLICATION raw = new AcsApplicationBO().Get<ACS_APPLICATION>(entity.ID);
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
                    result = DAOWorker.AcsApplicationDAO.Update(raw);
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
