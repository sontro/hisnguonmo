using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.ListV
{
    class AcsRoleUserGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsRoleUserGetListV
    {
        AcsRoleUserViewFilterQuery filterQuery;

        internal AcsRoleUserGetListVBehaviorByViewFilterQuery(CommonParam param, AcsRoleUserViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_ROLE_USER> IAcsRoleUserGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsRoleUserDAO.GetView(filterQuery.Query(), param);
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
