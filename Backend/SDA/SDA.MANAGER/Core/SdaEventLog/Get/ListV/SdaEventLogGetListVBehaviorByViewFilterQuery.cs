using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaEventLog.Get.ListV
{
    class SdaEventLogGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaEventLogGetListV
    {
        SdaEventLogViewFilterQuery filterQuery;

        internal SdaEventLogGetListVBehaviorByViewFilterQuery(CommonParam param, SdaEventLogViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_EVENT_LOG> ISdaEventLogGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaEventLogDAO.GetView(filterQuery.Query(), param);
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
