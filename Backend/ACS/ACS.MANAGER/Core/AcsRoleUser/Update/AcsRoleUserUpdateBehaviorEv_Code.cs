using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser.Update
{
    class AcsRoleUserUpdateBehaviorEv : BeanObjectBase, IAcsRoleUserUpdate
    {
        ACS_ROLE_USER entity;

        internal AcsRoleUserUpdateBehaviorEv(CommonParam param, ACS_ROLE_USER data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleUserUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleUserDAO.Update(entity);
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
                result = result && AcsRoleUserCheckVerifyIsUnlock.Verify(param, entity.ID);
                result = result && AcsRoleUserCheckVerifyExistsCode.Verify(param, entity.ROLE_USER_CODE, entity.ID);
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
