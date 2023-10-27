using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole.Create
{
    class AcsApplicationRoleCreateBehaviorEv : BeanObjectBase, IAcsApplicationRoleCreate
    {
        ACS_APPLICATION_ROLE entity;

        internal AcsApplicationRoleCreateBehaviorEv(CommonParam param, ACS_APPLICATION_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsApplicationRoleCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsApplicationRoleDAO.Create(entity);
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
                result = result && AcsApplicationRoleCheckVerifyValidData.Verify(param, entity);
                result = result && AcsApplicationRoleCheckVerifyExistsCode.Verify(param, entity.APPLICATION_ROLE_CODE, null);
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
