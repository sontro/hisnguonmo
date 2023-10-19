using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole.Get.V
{
    class AcsModuleRoleGetVBehaviorById : BeanObjectBase, IAcsModuleRoleGetV
    {
        long id;

        internal AcsModuleRoleGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_MODULE_ROLE IAcsModuleRoleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsModuleRoleDAO.GetViewById(id, new AcsModuleRoleViewFilterQuery().Query());
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
