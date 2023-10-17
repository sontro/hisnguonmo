using Inventec.Common.Logging;
using Inventec.Token.ResourceSystem;
using MOS.UTILITY;
using SDA.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LogManager
{
    public class EventLogCache
    {
        private static List<SdaEventLogSDO> eventLogs = new List<SdaEventLogSDO>();

        public static bool Push(string description)
        {
            bool result = false;
            try
            {
                SdaEventLogSDO eventLog = new SdaEventLogSDO();
                eventLog.AppCode = Constant.APPLICATION_CODE;
                try
                {
                    eventLog.Ip = ResourceTokenManager.GetRequestAddress();
                }
                catch (Exception ex)
                {
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                eventLog.LogginName = ResourceTokenManager.GetLoginName();
                eventLog.EventTime = Inventec.Common.DateTime.Get.Now().Value; ;
                eventLog.Description = description;
                eventLogs.Add(eventLog);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static bool Push(List<SdaEventLogSDO> eventLog)
        {
            bool result = false;
            try
            {
                eventLogs.AddRange(eventLog);
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        public static List<SdaEventLogSDO> Pop()
        {
            List<SdaEventLogSDO> result = new List<SdaEventLogSDO>();
            try
            {
                result.AddRange(eventLogs);
                eventLogs.Clear();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }
    }
}
