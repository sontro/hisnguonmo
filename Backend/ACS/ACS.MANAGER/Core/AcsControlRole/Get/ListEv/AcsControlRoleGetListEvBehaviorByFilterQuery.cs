using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsControlRole.Get.ListEv
{
    class AcsControlRoleGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsControlRoleGetListEv
    {
        AcsControlRoleFilterQuery filterQuery;

        internal AcsControlRoleGetListEvBehaviorByFilterQuery(CommonParam param, AcsControlRoleFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_CONTROL_ROLE> IAcsControlRoleGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsControlRoleDAO.Get(filterQuery.Query(), param);
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
