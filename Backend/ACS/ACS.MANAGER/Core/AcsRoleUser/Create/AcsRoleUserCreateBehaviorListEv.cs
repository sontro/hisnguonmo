using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.Create
{
    class AcsRoleUserCreateBehaviorListEv : BeanObjectBase, IAcsRoleUserCreate
    {
        List<ACS_ROLE_USER> entities;

        internal AcsRoleUserCreateBehaviorListEv(CommonParam param, List<ACS_ROLE_USER> datas)
            : base(param)
        {
            entities = datas;
        }

        bool IAcsRoleUserCreate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsRoleUserDAO.CreateList(entities);
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
                result = result && AcsRoleUserCheckVerifyValidData.Verify(param, entities);
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
