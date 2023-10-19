using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.Get.Ev
{
    class AcsModuleGroupGetEvBehaviorByCode : BeanObjectBase, IAcsModuleGroupGetEv
    {
        string code;

        internal AcsModuleGroupGetEvBehaviorByCode(CommonParam param, string filter)
            : base(param)
        {
            code = filter;
        }

        ACS_MODULE_GROUP IAcsModuleGroupGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsModuleGroupDAO.GetByCode(code, new AcsModuleGroupFilterQuery().Query());
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
