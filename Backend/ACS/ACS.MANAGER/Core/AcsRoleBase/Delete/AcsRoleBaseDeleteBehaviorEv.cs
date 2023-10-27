using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRoleBase.Delete
{
    class AcsRoleBaseDeleteBehaviorEv : BeanObjectBase, IAcsRoleBaseDelete
    {
        ACS_ROLE_BASE entity;

        internal AcsRoleBaseDeleteBehaviorEv(CommonParam param, ACS_ROLE_BASE data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleBaseDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleBaseDAO.Truncate(entity);
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
                result = result && AcsRoleBaseCheckVerifyIsUnlock.Verify(param, entity.ID);
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
