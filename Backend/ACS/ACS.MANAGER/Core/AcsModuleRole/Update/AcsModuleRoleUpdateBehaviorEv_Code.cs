using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole.Update
{
    class AcsModuleRoleUpdateBehaviorEv : BeanObjectBase, IAcsModuleRoleUpdate
    {
        ACS_MODULE_ROLE entity;

        internal AcsModuleRoleUpdateBehaviorEv(CommonParam param, ACS_MODULE_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleRoleUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleRoleDAO.Update(entity);
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
                result = result && AcsModuleRoleCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && AcsModuleRoleCheckVerifyExistsCode.Verify(param, entity.MODULE_ROLE_CODE, entity.ID);
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
