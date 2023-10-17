using HIS.Desktop.LocalStorage.ConfigSystem;
using HIS.Desktop.LocalStorage.LocalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoom.PACS
{
    class PacsApiConsumer
    {
        private static Inventec.Common.WebApiClient.ApiConsumer pacsConsumer;
        internal static Inventec.Common.WebApiClient.ApiConsumer PacsConsumer
        {
            get
            {
                if (pacsConsumer == null)
                {
                    pacsConsumer = new Inventec.Common.WebApiClient.ApiConsumer(ConfigSystems.URI_API_PACS, GlobalVariables.APPLICATION_CODE);
                }
                return pacsConsumer;
            }
            set
            {
                pacsConsumer = value;
            }
        }
    }
}
