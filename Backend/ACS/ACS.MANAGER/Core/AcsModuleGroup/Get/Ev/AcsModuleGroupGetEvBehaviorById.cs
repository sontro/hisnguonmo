using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;

namespace ACS.MANAGER.Core.AcsModuleGroup.Get.Ev
{
    class AcsModuleGroupGetEvBehaviorById : BeanObjectBase, IAcsModuleGroupGetEv
    {
        long id;

        internal AcsModuleGroupGetEvBehaviorById(CommonParam param, long filter)
            : base(param)
        {
            id = filter;
        }

        ACS_MODULE_GROUP IAcsModuleGroupGetEv.Run()
        {
            try
            {
                return DAOWorker.AcsModuleGroupDAO.GetById(id, new AcsModuleGroupFilterQuery().Query());
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
