using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleRole.Get.V
{
    class AcsModuleRoleGetVBehaviorByCode : BeanObjectBase, IAcsModuleRoleGetV
    {
        string code;

        internal AcsModuleRoleGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_MODULE_ROLE IAcsModuleRoleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsModuleRoleDAO.GetViewByCode(code, new AcsModuleRoleViewFilterQuery().Query());
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
