using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AcsUser
{
    class ApiConsumers
    {
        public static Inventec.Common.WebApiClient.ApiConsumer SdaConsumer { get; set; }
        public static Inventec.Common.WebApiClient.ApiConsumer AcsConsumer { get; set; }
    }
}
