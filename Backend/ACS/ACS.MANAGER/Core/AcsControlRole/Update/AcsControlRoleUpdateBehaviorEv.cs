using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole.Update
{
    class AcsControlRoleUpdateBehaviorEv : BeanObjectBase, IAcsControlRoleUpdate
    {
        ACS_CONTROL_ROLE entity;

        internal AcsControlRoleUpdateBehaviorEv(CommonParam param, ACS_CONTROL_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsControlRoleUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsControlRoleDAO.Update(entity);
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
                result = result && AcsControlRoleCheckVerifyValidData.Verify(param, entity);
                result = result && AcsControlRoleCheckVerifyIsUnlock.Verify(param, entity.ID);
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
