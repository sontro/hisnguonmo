using ACS.EFMODEL.DataModels;
using ACS.LibraryEventLog;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.EventLogUtil;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModule.Delete
{
    class AcsModuleDeleteBehaviorEv : BeanObjectBase, IAcsModuleDelete
    {
        ACS_MODULE entity;

        internal AcsModuleDeleteBehaviorEv(CommonParam param, ACS_MODULE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleDAO.Truncate(entity);
                if (result)
                {
                    CreateEventLog();
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

        bool Check()
        {
            bool result = true;
            try
            {
                result = result && AcsModuleCheckVerifyIsUnlock.Verify(param, entity.ID);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void CreateEventLog()
        {
            try
            {
                new EventLogGenerator(EventLog.Enum.AcsModule_XoaDanhMucChucNang, String.Format("MODULE_NAME: {0}", entity.MODULE_NAME))
                          .ModuleLink(entity.MODULE_LINK).Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
