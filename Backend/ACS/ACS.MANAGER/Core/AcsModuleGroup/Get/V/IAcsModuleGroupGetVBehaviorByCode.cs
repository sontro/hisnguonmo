using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.Get.V
{
    class AcsModuleGroupGetVBehaviorByCode : BeanObjectBase, IAcsModuleGroupGetV
    {
        string code;

        internal AcsModuleGroupGetVBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        V_ACS_MODULE_GROUP IAcsModuleGroupGetV.Run()
        {
            try
            {
                return DAOWorker.AcsModuleGroupDAO.GetViewByCode(code, new AcsModuleGroupViewFilterQuery().Query());
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
