using Inventec.Common.Logging;
using Inventec.Core;
using MOS.ApiConsumerManager;
using MOS.MANAGER.Base;
using SDA.EFMODEL.DataModels;
using SDA.Filter;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.SdaSqlParam
{
    class SdaSqlParamGet : BusinessBase
    {
        internal SdaSqlParamGet()
            : base()
        {

        }

        internal SdaSqlParamGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<SDA_SQL_PARAM> Get(SdaSqlParamFilter filter)
        {
            try
            {
                ApiConsumerStore.SdaConsumer.SetTokenCode(Inventec.Token.ResourceSystem.ResourceTokenManager.GetTokenCode());
                var ro = ApiConsumerStore.SdaConsumer.Get<Inventec.Core.ApiResultObject<List<SDA_SQL_PARAM>>>("/api/SdaSqlParam/Get", param, filter);
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
