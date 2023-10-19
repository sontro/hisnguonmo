using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole.Delete
{
    class AcsApplicationRoleDeleteBehaviorEv : BeanObjectBase, IAcsApplicationRoleDelete
    {
        ACS_APPLICATION_ROLE entity;

        internal AcsApplicationRoleDeleteBehaviorEv(CommonParam param, ACS_APPLICATION_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsApplicationRoleDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsApplicationRoleDAO.Truncate(entity);
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
                result = result && AcsApplicationRoleCheckVerifyIsUnlock.Verify(param, entity.ID);
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
