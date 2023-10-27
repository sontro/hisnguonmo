using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.Core.AcsActivityLog.Get.ListV
{
    class AcsActivityLogGetListVBehaviorByViewFilterQuery : BeanObjectBase, IAcsActivityLogGetListV
    {
        AcsActivityLogViewFilterQuery filterQuery;

        internal AcsActivityLogGetListVBehaviorByViewFilterQuery(CommonParam param, AcsActivityLogViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_ACS_ACTIVITY_LOG> IAcsActivityLogGetListV.Run()
        {
            try
            {
                return DAOWorker.AcsActivityLogDAO.GetView(filterQuery.Query(), param);
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
