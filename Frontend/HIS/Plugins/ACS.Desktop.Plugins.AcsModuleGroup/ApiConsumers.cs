using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Desktop.Plugins.AcsModuleGroup
{
    class ApiConsumers
    {
        public static Inventec.Common.WebApiClient.ApiConsumer SdaConsumer { get; set; }
        public static Inventec.Common.WebApiClient.ApiConsumer AcsConsumer { get; set; }
    }
}
