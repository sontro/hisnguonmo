using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRole.RoleBase.Get.ListEv
{
    class AcsRoleRoleBaseGetListEvBaseBehaviorByFilterQuery : BeanObjectBase, IAcsRoleRoleBaseGetListEv
    {
        AcsRoleListBaseFilterQuery filterQuery;

        internal AcsRoleRoleBaseGetListEvBaseBehaviorByFilterQuery(CommonParam param, AcsRoleListBaseFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_ROLE_BASE> IAcsRoleRoleBaseGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsRoleBaseDAO.Get(filterQuery.Query(), param);
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
