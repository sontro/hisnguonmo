using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsActivityType.Get.ListEv
{
    class AcsActivityTypeGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsActivityTypeGetListEv
    {
        AcsActivityTypeFilterQuery filterQuery;

        internal AcsActivityTypeGetListEvBehaviorByFilterQuery(CommonParam param, AcsActivityTypeFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_ACTIVITY_TYPE> IAcsActivityTypeGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsActivityTypeDAO.Get(filterQuery.Query(), param);
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
