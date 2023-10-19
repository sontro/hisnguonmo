using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsModuleGroup.Get.ListV
{
    class AcsModuleGroupGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsModuleGroupGetListV
    {
        AcsModuleGroupViewFilterQuery filterQuery;

        internal AcsModuleGroupGetListVBehaviorByViewFilterQuery(CommonParam param, AcsModuleGroupViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_MODULE_GROUP> IAcsModuleGroupGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsModuleGroupDAO.GetView(filterQuery.Query(), param);
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
