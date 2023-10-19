using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using ACS.MANAGER.Core.AcsModuleGroup.EventLog;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.Update
{
    class AcsModuleGroupUpdateBehaviorEv : BeanObjectBase, IAcsModuleGroupUpdate
    {
        ACS_MODULE_GROUP current;
        ACS_MODULE_GROUP entity;

        internal AcsModuleGroupUpdateBehaviorEv(CommonParam param, ACS_MODULE_GROUP data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleGroupUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleGroupDAO.Update(entity);
                if (result) { AcsModuleGroupEventLogUpdate.Log(current, entity); }
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
                result = result && AcsModuleGroupCheckVerifyIsUnlock.Verify(param, entity.ID, ref current);
                result = result && AcsModuleGroupCheckVerifyExistsCode.Verify(param, entity.MODULE_GROUP_CODE, entity.ID);
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
