using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRole.Get.ListV
{
    class AcsRoleGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsRoleGetListV
    {
        AcsRoleViewFilterQuery filterQuery;

        internal AcsRoleGetListVBehaviorByViewFilterQuery(CommonParam param, AcsRoleViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_ROLE> IAcsRoleGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsRoleDAO.GetView(filterQuery.Query(), param);
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
