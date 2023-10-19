using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleBase.Get.ListV
{
    class AcsRoleBaseGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsRoleBaseGetListV
    {
        AcsRoleBaseViewFilterQuery filterQuery;

        internal AcsRoleBaseGetListVBehaviorByViewFilterQuery(CommonParam param, AcsRoleBaseViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_ROLE_BASE> IAcsRoleBaseGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsRoleBaseDAO.GetView(filterQuery.Query(), param);
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
