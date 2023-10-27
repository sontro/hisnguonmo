using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.Delete
{
    class AcsRoleUserDeleteBehaviorListEv : BeanObjectBase, IAcsRoleUserDelete
    {
        List<ACS_ROLE_USER> entity;

        internal AcsRoleUserDeleteBehaviorListEv(CommonParam param, List<ACS_ROLE_USER> data)
            : base(param)
        {
            entity = data;
        }

        bool IAcsRoleUserDelete.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleUserDAO.TruncateList(entity);
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
                foreach (var item in entity)
                {
                    result = result && AcsRoleUserCheckVerifyIsUnlock.Verify(param, item.ID);
                }
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
