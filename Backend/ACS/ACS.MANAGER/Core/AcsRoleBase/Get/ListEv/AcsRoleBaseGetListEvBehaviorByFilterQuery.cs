using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsRoleBase.Get.ListEv
{
    class AcsRoleBaseGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsRoleBaseGetListEv
    {
        AcsRoleBaseFilterQuery filterQuery;

        internal AcsRoleBaseGetListEvBehaviorByFilterQuery(CommonParam param, AcsRoleBaseFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_ROLE_BASE> IAcsRoleBaseGetListEv.Run()
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
