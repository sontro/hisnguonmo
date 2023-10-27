using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleUser.Get.ListEv
{
    class AcsRoleUserGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsRoleUserGetListEv
    {
        AcsRoleUserFilterQuery filterQuery;

        internal AcsRoleUserGetListEvBehaviorByFilterQuery(CommonParam param, AcsRoleUserFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_ROLE_USER> IAcsRoleUserGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsRoleUserDAO.Get(filterQuery.Query(), param);
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
