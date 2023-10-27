using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsControlRole.Get.ListV
{
    class AcsControlRoleGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsControlRoleGetListV
    {
        AcsControlRoleViewFilterQuery filterQuery;

        internal AcsControlRoleGetListVBehaviorByViewFilterQuery(CommonParam param, AcsControlRoleViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_CONTROL_ROLE> IAcsControlRoleGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsControlRoleDAO.GetView(filterQuery.Query(), param);
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
