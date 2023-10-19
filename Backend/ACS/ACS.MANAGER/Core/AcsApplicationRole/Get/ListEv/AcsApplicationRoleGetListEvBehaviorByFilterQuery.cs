using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsApplicationRole.Get.ListEv
{
    class AcsApplicationRoleGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsApplicationRoleGetListEv
    {
        AcsApplicationRoleFilterQuery filterQuery;

        internal AcsApplicationRoleGetListEvBehaviorByFilterQuery(CommonParam param, AcsApplicationRoleFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_APPLICATION_ROLE> IAcsApplicationRoleGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsApplicationRoleDAO.Get(filterQuery.Query(), param);
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
