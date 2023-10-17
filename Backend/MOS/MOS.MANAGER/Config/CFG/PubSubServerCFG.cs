using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class PubSubServerCFG
    {
        private const string PubSubServerInfo = "HIS.Desktop.PubSubServerInfo";
        private const string TimeCheckConnection = "HIS.Desktop.PubSub.TimeCheckConnection";

        private static string pubSubServerInfo;
        public static string PUB_SUB_SERVER_INFO
        {
            get
            {
                if (pubSubServerInfo == null)
                {
                    pubSubServerInfo = ConfigUtil.GetStrConfig(PubSubServerInfo);
                }
                return pubSubServerInfo;
            }
            set
            {
                pubSubServerInfo = value;
            }
        }

        private static long? timeCheckConnection;
        public static long Time_Check_Connection
        {
            get
            {
                if (timeCheckConnection == null)
                {
                    timeCheckConnection = ConfigUtil.GetLongConfig(TimeCheckConnection);
                }
                return timeCheckConnection.Value;
            }
            set
            {
                timeCheckConnection = value;
            }
        }

        public static void Reload()
        {
            pubSubServerInfo = ConfigUtil.GetStrConfig(PubSubServerInfo);
            timeCheckConnection = ConfigUtil.GetLongConfig(TimeCheckConnection);
        }
    }
}
