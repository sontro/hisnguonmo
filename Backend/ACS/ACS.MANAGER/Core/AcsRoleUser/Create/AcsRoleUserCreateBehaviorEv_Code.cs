using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser.Create
{
    class AcsRoleUserCreateBehaviorEv : BeanObjectBase, IAcsRoleUserCreate
    {
        ACS_ROLE_USER entity;

        internal AcsRoleUserCreateBehaviorEv(CommonParam param, ACS_ROLE_USER data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleUserCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleUserDAO.Create(entity);
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
                result = result && AcsRoleUserCheckVerifyValidData.Verify(param, entity);
                result = result && AcsRoleUserCheckVerifyExistsCode.Verify(param, entity.ROLE_USER_CODE, null);
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
