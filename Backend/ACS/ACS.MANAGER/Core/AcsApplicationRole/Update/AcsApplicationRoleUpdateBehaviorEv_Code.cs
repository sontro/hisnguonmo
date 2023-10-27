using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole.Update
{
    class AcsApplicationRoleUpdateBehaviorEv : BeanObjectBase, IAcsApplicationRoleUpdate
    {
        ACS_APPLICATION_ROLE entity;

        internal AcsApplicationRoleUpdateBehaviorEv(CommonParam param, ACS_APPLICATION_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsApplicationRoleUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsApplicationRoleDAO.Update(entity);
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
                result = result && AcsApplicationRoleCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && AcsApplicationRoleCheckVerifyExistsCode.Verify(param, entity.APPLICATION_ROLE_CODE, entity.ID);
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
