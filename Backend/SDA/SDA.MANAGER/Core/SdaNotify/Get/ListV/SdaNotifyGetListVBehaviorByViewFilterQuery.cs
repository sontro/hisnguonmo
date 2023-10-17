using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.MANAGER.Core.SdaNotify.Get.ListV
{
    class SdaNotifyGetListVBehaviorByViewFilterQuery : BeanObjectBase, ISdaNotifyGetListV
    {
        SdaNotifyViewFilterQuery filterQuery;

        internal SdaNotifyGetListVBehaviorByViewFilterQuery(CommonParam param, SdaNotifyViewFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<V_SDA_NOTIFY> ISdaNotifyGetListV.Run()
        {
            try
            {
                return DAOWorker.SdaNotifyDAO.GetView(filterQuery.Query(), param);
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
