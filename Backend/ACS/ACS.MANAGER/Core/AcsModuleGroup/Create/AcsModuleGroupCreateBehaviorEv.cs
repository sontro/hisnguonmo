using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.Core.AcsModuleGroup.EventLog;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.Create
{
    class AcsModuleGroupCreateBehaviorEv : BeanObjectBase, IAcsModuleGroupCreate
    {
        ACS_MODULE_GROUP entity;

        internal AcsModuleGroupCreateBehaviorEv(CommonParam param, ACS_MODULE_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleGroupCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleGroupDAO.Create(entity);
                if (result) { AcsModuleGroupEventLogCreate.Log(entity); }
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
                result = result && AcsModuleGroupCheckVerifyValidData.Verify(param, entity);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
