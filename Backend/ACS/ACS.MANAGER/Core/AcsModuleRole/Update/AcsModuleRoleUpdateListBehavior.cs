using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using ACS.MANAGER.Core.Check;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleRole.Update
{
    class AcsModuleRoleUpdateListBehavior : BeanObjectBase, IAcsModuleRoleUpdate
    {
        List<ACS_MODULE_ROLE> entitys;

        internal AcsModuleRoleUpdateListBehavior(CommonParam param, List<ACS_MODULE_ROLE> data)
            : base(param)
        {
            entitys = data;
        }

        bool IAcsModuleRoleUpdate.Run()
        {
            bool result = false;
            try
            {
                result = Check() && DAOWorker.AcsModuleRoleDAO.UpdateList(entitys);
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
                foreach (var entity in entitys)
                {
                    result = result && AcsModuleRoleCheckVerifyValidData.Verify(param, entity);
                    result = result && AcsModuleRoleCheckVerifyIsUnlock.Verify(param, entity.ID);
                }

                //result = result && AcsModuleRoleCheckVerifyExistsCode.Verify(param, entity.MODULE_ROLE_CODE, entity.ID);
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
