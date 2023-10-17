using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Library.CacheClient
{
    public class ApiConsumers
    {
        public static Inventec.Common.WebApiClient.ApiConsumer MosConsumer
        {
            get
            {
                return new Inventec.Common.WebApiClient.ApiConsumer(SerivceConfig.MosBaseUri, SerivceConfig.TokenCode, SerivceConfig.ApplicationCode);
            }
        }

        private static Inventec.Common.WebApiClient.ApiConsumer sdaConsumer;
        public static Inventec.Common.WebApiClient.ApiConsumer SdaConsumer
        {
            get
            {
                if (sdaConsumer == null)
                {
                    sdaConsumer = new Inventec.Common.WebApiClient.ApiConsumer(SerivceConfig.SdaBaseUri, SerivceConfig.TokenCode, SerivceConfig.ApplicationCode);
                }
                return sdaConsumer;
            }
            set
            {
                sdaConsumer = value;
            }
        }

        private static Inventec.Common.WebApiClient.ApiConsumer acsConsumer;
        public static Inventec.Common.WebApiClient.ApiConsumer AcsConsumer
        {
            get
            {
                if (acsConsumer == null)
                {
                    acsConsumer = new Inventec.Common.WebApiClient.ApiConsumer(SerivceConfig.AcsBaseUri, SerivceConfig.TokenCode, SerivceConfig.ApplicationCode);
                }
                return acsConsumer;
            }
            set
            {
                acsConsumer = value;
            }
        }

        private static Inventec.Common.WebApiClient.ApiConsumer sarConsumer;
        public static Inventec.Common.WebApiClient.ApiConsumer SarConsumer
        {
            get
            {
                if (sarConsumer == null)
                {
                    sarConsumer = new Inventec.Common.WebApiClient.ApiConsumer(SerivceConfig.SarBaseUri, SerivceConfig.TokenCode, SerivceConfig.ApplicationCode);
                }
                return sarConsumer;
            }
            set
            {
                sarConsumer = value;
            }
        }
    }
}
