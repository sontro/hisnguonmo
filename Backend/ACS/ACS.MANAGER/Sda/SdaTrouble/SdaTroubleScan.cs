using ACS.ApiConsumerManager;
using ACS.MANAGER.Base;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace ACS.MANAGER.SdaTrouble
{
    class SdaTroubleScan  : Inventec.Backend.MANAGER.BusinessBase
    {
        internal SdaTroubleScan()
            : base()
        {

        }

        internal SdaTroubleScan(CommonParam paramScan)
            : base(paramScan)
        {

        }

        internal void Execute()
        {
            try
            {
                List<string> listTrouble = TroubleCache.GetAndClear();
                Inventec.Core.ApiResultObject<bool> aro = ApiConsumerStore.SdaConsumer.Post<Inventec.Core.ApiResultObject<bool>>("/api/SdaTrouble/Create", param, listTrouble);
                if (!(aro != null && aro.Success))
                {
                    Logging("Khong insert duoc du lieu trouble.", LogType.Error);
                    Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => aro), aro));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
