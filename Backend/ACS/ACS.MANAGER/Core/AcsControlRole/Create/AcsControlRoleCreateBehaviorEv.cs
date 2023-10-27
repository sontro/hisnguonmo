using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole.Create
{
    class AcsControlRoleCreateBehaviorEv : BeanObjectBase, IAcsControlRoleCreate
    {
        ACS_CONTROL_ROLE entity;

        internal AcsControlRoleCreateBehaviorEv(CommonParam param, ACS_CONTROL_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsControlRoleCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsControlRoleDAO.Create(entity);
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
