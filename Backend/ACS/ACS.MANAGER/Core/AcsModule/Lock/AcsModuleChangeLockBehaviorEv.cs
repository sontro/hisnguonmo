using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using ACS.MANAGER.Core.AcsModule;
using ACS.MANAGER.EventLogUtil;
using ACS.LibraryEventLog;

namespace ACS.MANAGER.Core.AcsModule.Lock
{
    class AcsModuleChangeLockBehaviorEv : BeanObjectBase, IAcsModuleChangeLock
    {
        ACS_MODULE entity;

        internal AcsModuleChangeLockBehaviorEv(CommonParam param, ACS_MODULE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleChangeLock.Run()
        {
            bool result = false;
            try
            {
                ACS_MODULE raw = new AcsModuleBO().Get<ACS_MODULE>(entity.ID);
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
                    result = DAOWorker.AcsModuleDAO.Update(raw);
                    if (result)
                    {
                        entity.IS_ACTIVE = raw.IS_ACTIVE;
                        CreateEventLog(raw);
                    }
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

        private void CreateEventLog(ACS_MODULE raw)
        {
            try
            {
                new EventLogGenerator(raw.IS_ACTIVE == IMSys.DbConfig.ACS_RS.COMMON.IS_ACTIVE__TRUE ? EventLog.Enum.AcsModule_MoKhoaDanhMucChucNang : EventLog.Enum.AcsModule_KhoaDanhMucChucNang, String.Format("MODULE_NAME: {0}", raw.MODULE_NAME))
                          .ModuleLink(raw.MODULE_LINK).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
