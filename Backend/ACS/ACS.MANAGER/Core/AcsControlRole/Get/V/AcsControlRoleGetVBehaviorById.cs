using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole.Get.V
{
    class AcsControlRoleGetVBehaviorById : BeanObjectBase, IAcsControlRoleGetV
    {
        long id;

        internal AcsControlRoleGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_CONTROL_ROLE IAcsControlRoleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsControlRoleDAO.GetViewById(id, new AcsControlRoleViewFilterQuery().Query());
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
