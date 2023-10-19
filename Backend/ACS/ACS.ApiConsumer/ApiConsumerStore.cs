using Inventec.Common.WebApiClient;
using Inventec.Token.ResourceSystem;
using System.Configuration;

namespace ACS.ApiConsumerManager
{
    public class ApiConsumerStore
    {
        private static ApiConsumer sdaConsumer;
        public static ApiConsumer SdaConsumer
        {
            get
            {
                if (sdaConsumer == null)
                {
                    sdaConsumer = new ApiConsumer(ACS.LibraryConfig.WebConfig.URI_API_SDA, ACS.UTILITY.Constant.APPLICATION_CODE);
                }
                return sdaConsumer;
            }
        }

        public static ApiConsumer AcsConsumer
        {
            get
            {
                string tokenCode = ResourceTokenManager.GetTokenCode();
                return new ApiConsumer(ACS.LibraryConfig.WebConfig.URI_API_ACS, tokenCode, ACS.UTILITY.Constant.APPLICATION_CODE);
            }
        }

        public static ApiConsumer SmsConsumer
        {
            get
            {
                string tokenCode = ResourceTokenManager.GetTokenCode();
                return new ApiConsumer(ACS.LibraryConfig.WebConfig.URI_API_SMS, tokenCode, ACS.UTILITY.Constant.APPLICATION_CODE);
            }
        }
      
    }
}
