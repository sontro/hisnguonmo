using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vva.Desktop.Plugins.VvaVoiceCommand
{
    class ApiConsumers
    {
        public static Inventec.Common.WebApiClient.ApiConsumer VvaConsumer { get; set; }
        public static Inventec.Common.WebApiClient.ApiConsumer AcsConsumer { get; set; }
    }
}
