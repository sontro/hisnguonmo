using SDA.EFMODEL.DataModels;
using SAR.MANAGER.Base;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SAR.MANAGER.Core.SdaConfig.Get.ListEv
{
    class SdaConfigGetListEvBehaviorByFilterQuery : BeanObjectBase, ISdaConfigGetListEv
    {
        SdaConfigFilterQuery filterQuery;

        internal SdaConfigGetListEvBehaviorByFilterQuery(CommonParam param, SdaConfigFilterQuery filter)
            : base(param)
        {
            filterQuery = filter;
        }

        List<SDA_CONFIG> ISdaConfigGetListEv.Run()
        {
            try
            {
                var ro = ApiConsumerStore.SdaConsumer.Get<Inventec.Core.ApiResultObject<List<SDA_CONFIG>>>("/api/SdaConfig/Get", param, filterQuery);
                if (ro != null)
                {
                    param = ro.Param != null ? ro.Param : param;
                    return ro.Data;
                }
                else
                {
                    return null;
                }
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
