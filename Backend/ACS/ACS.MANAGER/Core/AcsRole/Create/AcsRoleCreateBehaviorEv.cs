using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole.Create
{
    class AcsRoleCreateBehaviorEv : BeanObjectBase, IAcsRoleCreate
    {
        ACS_ROLE entity;

        internal AcsRoleCreateBehaviorEv(CommonParam param, ACS_ROLE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleDAO.Create(entity);
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
