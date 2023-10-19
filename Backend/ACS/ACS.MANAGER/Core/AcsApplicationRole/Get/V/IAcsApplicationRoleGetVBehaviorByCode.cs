using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole.Get.V
{
    class AcsApplicationRoleGetVBehaviorByCode : BeanObjectBase, IAcsApplicationRoleGetV
    {
        string code;

        internal AcsApplicationRoleGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_APPLICATION_ROLE IAcsApplicationRoleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationRoleDAO.GetViewByCode(code, new AcsApplicationRoleViewFilterQuery().Query());
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
