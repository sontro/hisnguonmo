using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsApplicationRole.Get.V
{
    class AcsApplicationRoleGetVBehaviorById : BeanObjectBase, IAcsApplicationRoleGetV
    {
        long id;

        internal AcsApplicationRoleGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_APPLICATION_ROLE IAcsApplicationRoleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationRoleDAO.GetViewById(id, new AcsApplicationRoleViewFilterQuery().Query());
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
