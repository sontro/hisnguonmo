using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.MANAGER.Base;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.SdaConfig
{
    class SdaConfigGet : BusinessBase
    {
        internal SdaConfigGet()
            : base()
        {

        }

        internal SdaConfigGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<SDA_CONFIG> Get(SdaConfigFilter filter)
        {
            try
            {
                var ro = ApiConsumerStore.SdaConsumer.Get<Inventec.Core.ApiResultObject<List<SDA_CONFIG>>>("/api/SdaConfig/Get", param, filter);
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
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
