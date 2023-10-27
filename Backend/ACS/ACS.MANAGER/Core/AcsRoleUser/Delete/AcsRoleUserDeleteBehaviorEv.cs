using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleUser.Delete
{
    class AcsRoleUserDeleteBehaviorEv : BeanObjectBase, IAcsRoleUserDelete
    {
        ACS_ROLE_USER entity;

        internal AcsRoleUserDeleteBehaviorEv(CommonParam param, ACS_ROLE_USER data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleUserDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleUserDAO.Truncate(entity);
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
                result = result && AcsRoleUserCheckVerifyIsUnlock.Verify(param, entity.ID);
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
