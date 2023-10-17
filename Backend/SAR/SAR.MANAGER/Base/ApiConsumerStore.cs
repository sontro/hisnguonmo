
using Inventec.Common.Logging;
using System;
namespace SAR.MANAGER.Base
{
    class ApiConsumerStore
    {
        static string uriMrs = System.Configuration.ConfigurationManager.AppSettings["MANAGER.Base.ApiConsumerStore.Mrs"];

        private static Inventec.Common.WebApiClient.ApiConsumer sdaConsumer;
        internal static Inventec.Common.WebApiClient.ApiConsumer SdaConsumer
        {
            get
            {
                if (sdaConsumer == null)
                {
                    sdaConsumer = new Inventec.Common.WebApiClient.ApiConsumer(System.Configuration.ConfigurationManager.AppSettings["MANAGER.Base.ApiConsumerStore.Sda"], ApplicationConfig.APPLICATION_CODE);
                }
                return sdaConsumer;
            }
        }

        internal static Inventec.Common.WebApiClient.ApiConsumer MrsConsumer
        {
            get
            {
                return new Inventec.Common.WebApiClient.ApiConsumer(uriMrs, ApplicationConfig.APPLICATION_CODE);
            }
        }
    }
}
