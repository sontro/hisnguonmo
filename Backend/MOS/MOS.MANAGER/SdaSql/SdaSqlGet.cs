using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.MANAGER.Base;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.SdaSql
{
    class SdaSqlGet : BusinessBase
    {
        internal SdaSqlGet()
            : base()
        {

        }

        internal SdaSqlGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<SDA_SQL> Get(SdaSqlFilter filter)
        {
            try
            {
                ApiConsumerStore.SdaConsumer.SetTokenCode(Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenCode());
                var ro = ApiConsumerStore.SdaConsumer.Get<Inventec.Core.ApiResultObject<List<SDA_SQL>>>("/api/SdaSql/Get", param, filter);
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
