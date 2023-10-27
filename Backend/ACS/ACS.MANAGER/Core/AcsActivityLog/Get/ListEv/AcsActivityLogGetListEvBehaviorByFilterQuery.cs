using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsActivityLog.Get.ListEv
{
    class AcsActivityLogGetListEvBehaviorByFilterQuery : BeanObjectBase, IAcsActivityLogGetListEv
    {
        AcsActivityLogFilterQuery filterQuery;

        internal AcsActivityLogGetListEvBehaviorByFilterQuery(CommonParam param, AcsActivityLogFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<ACS_ACTIVITY_LOG> IAcsActivityLogGetListEv.Run()
        {
            try
            {
                return DAOWorker.AcsActivityLogDAO.Get(filterQuery.Query(), param);
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
