using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.SdaTrouble
{
    class SdaTroubleGet  : Inventec.Backend.MANAGER.BusinessBase
    {
        internal SdaTroubleGet()
            : base()
        {

        }

        internal SdaTroubleGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<SDA_TROUBLE> Get(SdaTroubleFilter filter)
        {
            try
            {
                var aro = ACS.ApiConsumerManager.ApiConsumerStore.SdaConsumer.Get<Inventec.Core.ApiResultObject<List<SDA_TROUBLE>>>("/api/SdaTrouble/Get", param, filter);
                if (aro != null)
                {
                    param = aro.Param != null ? aro.Param : param;
                    return aro.Data;
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
