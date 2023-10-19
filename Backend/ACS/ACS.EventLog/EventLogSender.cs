using ACS.ApiConsumerManager;
using Inventec.Common.Logging;
using Inventec.Core;
//using MOS.ApiConsumerManager;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.LogManager
{
    public class EventLogSender
    {
        public static void Run()
        {
            try
            {
                List<SdaEventLogSDO> eventLogs = EventLogCache.Pop();

                LogSystem.Info("eventLogs count: " + (eventLogs != null ? eventLogs.Count : 0));

                if (eventLogs != null && eventLogs.Count > 0)
                {
                    var result = ApiConsumerStore.SdaConsumer.Post<ApiResultObject<bool>>("/api/SdaEventLog/CreateList", new CommonParam(), eventLogs);
                    if (!result.Success)
                    {
                        EventLogCache.Push(eventLogs);
                        LogSystem.Error("Gui thong tin log len he thong SDA that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
