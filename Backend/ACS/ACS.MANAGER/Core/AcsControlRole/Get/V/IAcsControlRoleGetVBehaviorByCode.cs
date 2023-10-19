using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsControlRole.Get.V
{
    class AcsControlRoleGetVBehaviorByCode : BeanObjectBase, IAcsControlRoleGetV
    {
        string code;

        internal AcsControlRoleGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_CONTROL_ROLE IAcsControlRoleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsControlRoleDAO.GetViewByCode(code, new AcsControlRoleViewFilterQuery().Query());
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
