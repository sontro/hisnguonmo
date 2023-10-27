using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole.Get.V
{
    class AcsRoleGetVBehaviorById : BeanObjectBase, IAcsRoleGetV
    {
        long id;

        internal AcsRoleGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_ROLE IAcsRoleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsRoleDAO.GetViewById(id, new AcsRoleViewFilterQuery().Query());
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
