using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole.Create
{
    class AcsModuleRoleCreateBehaviorEv : BeanObjectBase, IAcsModuleRoleCreate
    {
        ACS_MODULE_ROLE entity;

        internal AcsModuleRoleCreateBehaviorEv(CommonParam param, ACS_MODULE_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleRoleCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleRoleDAO.Create(entity);
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
                result = result && AcsModuleRoleCheckVerifyValidData.Verify(param, entity);
                result = result && AcsModuleRoleCheckVerifyExistsCode.Verify(param, entity.MODULE_ROLE_CODE, null);
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
