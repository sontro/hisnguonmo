using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisUserEditableData
{
    class ApiConsumers
    {
        public static Inventec.Common.WebApiClient.ApiConsumer mosConsumer { get; set; }
        public static Inventec.Common.WebApiClient.ApiConsumer acsConsumer { get; set; }
        public static Inventec.Common.WebApiClient.ApiConsumer sdaConsumer { get; set; }
    }
}
