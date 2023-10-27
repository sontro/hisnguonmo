using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleGroup.Get.ListEv
{
    class AcsModuleGroupGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsModuleGroupGetListEv
    {
        AcsModuleGroupFilterQuery filterQuery;

        internal AcsModuleGroupGetListEvBehaviorByFilterQuery(CommonParam param, AcsModuleGroupFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_MODULE_GROUP> IAcsModuleGroupGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsModuleGroupDAO.Get(filterQuery.Query(), param);
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
