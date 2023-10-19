using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplicationRole.Get.ListV
{
    class AcsApplicationRoleGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsApplicationRoleGetListV
    {
        AcsApplicationRoleViewFilterQuery filterQuery;

        internal AcsApplicationRoleGetListVBehaviorByViewFilterQuery(CommonParam param, AcsApplicationRoleViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_APPLICATION_ROLE> IAcsApplicationRoleGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationRoleDAO.GetView(filterQuery.Query(), param);
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
