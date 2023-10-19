using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsRole.Get.V
{
    class AcsRoleGetVBehaviorByCode : BeanObjectBase, IAcsRoleGetV
    {
        string code;

        internal AcsRoleGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_ROLE IAcsRoleGetV.Run()
        {
            try
            {
                return DAOWorker.AcsRoleDAO.GetViewByCode(code, new AcsRoleViewFilterQuery().Query());
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
