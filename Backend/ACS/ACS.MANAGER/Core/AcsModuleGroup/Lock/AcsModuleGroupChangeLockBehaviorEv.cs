using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.AcsModuleGroup.EventLog;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsModuleGroup;

namespace ACS.MANAGER.Core.AcsModuleGroup.Lock
{
    class AcsModuleGroupChangeLockBehaviorEv : BeanObjectBase, IAcsModuleGroupChangeLock
    {
        ACS_MODULE_GROUP entity;

        internal AcsModuleGroupChangeLockBehaviorEv(CommonParam param, ACS_MODULE_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleGroupChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_MODULE_GROUP raw = new AcsModuleGroupBO().Get<ACS_MODULE_GROUP>(entity.ID);
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
                    result = DAOWorker.AcsModuleGroupDAO.Update(raw);
                    if (result) 
                    {
                        if (raw.IS_ACTIVE == IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__FALSE)
                        {
                            AcsModuleGroupEventLogLock.Log(entity);
                        }
                        else
                        {
                            AcsModuleGroupEventLogUnLock.Log(entity);
                        }
                    }
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
