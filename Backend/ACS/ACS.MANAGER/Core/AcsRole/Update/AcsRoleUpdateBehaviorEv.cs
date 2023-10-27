using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole.Update
{
    class AcsRoleUpdateBehaviorEv : BeanObjectBase, IAcsRoleUpdate
    {
        ACS_ROLE entity;

        internal AcsRoleUpdateBehaviorEv(CommonParam param, ACS_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleDAO.Update(entity);
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
                result = result && AcsRoleCheckVerifyValidData.Verify(param, entity);
                result = result && AcsRoleCheckVerifyIsUnlock.Verify(param, entity.ID);
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
