using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.Core.AcsModuleGroup.EventLog;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.Delete
{
    class AcsModuleGroupDeleteBehaviorEv : BeanObjectBase, IAcsModuleGroupDelete
    {
        ACS_MODULE_GROUP entity;

        internal AcsModuleGroupDeleteBehaviorEv(CommonParam param, ACS_MODULE_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleGroupDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleGroupDAO.Truncate(entity);
                if (result) { AcsModuleGroupEventLogDelete.Log(entity); }
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
                result = result && AcsModuleGroupCheckVerifyIsUnlock.Verify(param, entity.ID, ref entity);
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
