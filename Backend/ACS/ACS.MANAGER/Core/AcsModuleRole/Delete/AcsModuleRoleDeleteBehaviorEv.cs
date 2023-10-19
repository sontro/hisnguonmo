using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole.Delete
{
    class AcsModuleRoleDeleteBehaviorEv : BeanObjectBase, IAcsModuleRoleDelete
    {
        ACS_MODULE_ROLE entity;

        internal AcsModuleRoleDeleteBehaviorEv(CommonParam param, ACS_MODULE_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsModuleRoleDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleRoleDAO.Truncate(entity);
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
                result = result && AcsModuleRoleCheckVerifyIsUnlock.Verify(param, entity.ID);
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
