using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.Get.V
{
    class AcsModuleGroupGetVBehaviorById : BeanObjectBase, IAcsModuleGroupGetV
    {
        long id;

        internal AcsModuleGroupGetVBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        V_ACS_MODULE_GROUP IAcsModuleGroupGetV.Run()
        {
            try
            {
                return DAOWorker.AcsModuleGroupDAO.GetViewById(id, new AcsModuleGroupViewFilterQuery().Query());
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
