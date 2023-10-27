using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase.Create
{
    class AcsRoleBaseCreateBehaviorEv : BeanObjectBase, IAcsRoleBaseCreate
    {
        ACS_ROLE_BASE entity;

        internal AcsRoleBaseCreateBehaviorEv(CommonParam param, ACS_ROLE_BASE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleBaseCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleBaseDAO.Create(entity);
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
                result = result && AcsRoleBaseCheckVerifyValidData.Verify(param, entity);
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
