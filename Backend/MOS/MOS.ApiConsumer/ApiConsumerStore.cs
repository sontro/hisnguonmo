using Inventec.Common.WebApiClient;
using Inventec.Token.ResourceSystem;
using System.Configuration;

namespace MOS.ApiConsumerManager
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
                    sdaConsumer = new ApiConsumer(ConfigurationManager.AppSettings["Inventec.SdaConsumer.Base.Uri"], MOS.UTILITY.Constant.APPLICATION_CODE);
                }
                return sdaConsumer;
            }
        }

        public static ApiConsumer AcsConsumer
        {
            get
            {
                string tokenCode = ResourceTokenManager.GetTokenCode();
                return new ApiConsumer(ConfigurationManager.AppSettings["Inventec.AcsConsumer.Base.Uri"], tokenCode, MOS.UTILITY.Constant.APPLICATION_CODE);
            }
        }

        private static ApiConsumerWrapper cosConsumer;
        public static ApiConsumerWrapper CosConsumer
        {
            get
            {
                if (cosConsumer == null)
                {
                    cosConsumer = new ApiConsumerWrapper(
                        true,
                        MOS.UTILITY.Constant.APPLICATION_CODE,
                        ConfigurationManager.AppSettings["Inventec.CosConsumer.Base.Uri"],
                        ConfigurationManager.AppSettings["Inventec.CosConsumer.Acs.Uri"],
                        ConfigurationManager.AppSettings["Inventec.CosConsumer.LoginName"],
                        ConfigurationManager.AppSettings["Inventec.CosConsumer.Password"]);
                    cosConsumer.UseRegistry(false);
                }
                return cosConsumer;
            }
        }

        private static ApiConsumerWrapper aosConsumer;
        public static ApiConsumerWrapper AosConsumer
        {
            get
            {
                if (aosConsumer == null)
                {
                    aosConsumer = new ApiConsumerWrapper(
                        true,
                        MOS.UTILITY.Constant.APPLICATION_CODE,
                        ConfigurationManager.AppSettings["Inventec.AosConsumer.Base.Uri"],
                        ConfigurationManager.AppSettings["Inventec.AosConsumer.Acs.Uri"],
                        ConfigurationManager.AppSettings["Inventec.AosConsumer.LoginName"],
                        ConfigurationManager.AppSettings["Inventec.AosConsumer.Password"]);
                    aosConsumer.UseRegistry(false);
                }
                return aosConsumer;
            }
        }

        private static ApiConsumer ehrLisConsumer;
        public static ApiConsumer EhrLisConsumer
        {
            get
            {
                if (ehrLisConsumer == null)
                {
                    string url = ConfigurationManager.AppSettings["Inventec.EhrLisConsumer.Base.Uri"];

                    if (!string.IsNullOrWhiteSpace(url))
                    {
                        ehrLisConsumer = new ApiConsumer(ConfigurationManager.AppSettings["Inventec.EhrLisConsumer.Base.Uri"], "BIIN");
                    }
                }
                return ehrLisConsumer;
            }
        }

        private static ApiConsumerWrapper smsConsumer;
        public static ApiConsumerWrapper SmsConsumer
        {
            get
            {
                if (smsConsumer == null)
                {
                    smsConsumer = new ApiConsumerWrapper(
                        true,
                        MOS.UTILITY.Constant.APPLICATION_CODE,
                        ConfigurationManager.AppSettings["Inventec.SmsConsumer.Sms.Uri"],
                        ConfigurationManager.AppSettings["Inventec.SmsConsumer.Acs.Uri"],
                        ConfigurationManager.AppSettings["Inventec.SmsConsumer.LoginName"],
                        ConfigurationManager.AppSettings["Inventec.SmsConsumer.Password"]);
                    smsConsumer.UseRegistry(false);
                }
                return smsConsumer;
            }
        }

        private static ApiConsumerWrapper emrConsumerWrapper;
        public static ApiConsumerWrapper EmrConsumerWrapper
        {
            get
            {
                if (emrConsumerWrapper == null)
                {
                    emrConsumerWrapper = new ApiConsumerWrapper(
                        true,
                        MOS.UTILITY.Constant.APPLICATION_CODE,
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.Emr.Uri"],
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.Acs.Uri"],
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.LoginName"],
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.Password"]);
                    emrConsumerWrapper.UseRegistry(false);
                }
                return emrConsumerWrapper;
            }
        }

        public static ApiConsumer EmrConsumer
        {
            get
            {
                string tokenCode = ResourceTokenManager.GetTokenCode();
                return new ApiConsumer(ConfigurationManager.AppSettings["Inventec.EmrConsumer.Emr.Uri"], tokenCode, MOS.UTILITY.Constant.APPLICATION_CODE);
            }
        }

        private static ApiConsumerWrapper sdaConsumerWrapper;
        public static ApiConsumerWrapper SdaConsumerWrapper
        {
            get
            {
                if (sdaConsumerWrapper == null)
                {
                    sdaConsumerWrapper = new ApiConsumerWrapper(
                        true,
                        MOS.UTILITY.Constant.APPLICATION_CODE,
                        ConfigurationManager.AppSettings["Inventec.SdaConsumer.Base.Uri"],
                        ConfigurationManager.AppSettings["Inventec.AcsConsumer.Base.Uri"],
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.LoginName"],
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.Password"]);
                    sdaConsumerWrapper.UseRegistry(false);
                }
                return sdaConsumerWrapper;
            }
        }

        private static ApiConsumerWrapper yttConsumer;
        public static ApiConsumerWrapper YttConsumer
        {
            get
            {
                if (yttConsumer == null)
                {
                    yttConsumer = new ApiConsumerWrapper(
                        true,
                        MOS.UTILITY.Constant.APPLICATION_CODE,
                        ConfigurationManager.AppSettings["Inventec.YttConsumer.Base.Uri"],
                        ConfigurationManager.AppSettings["Inventec.YttConsumer.Acs.Uri"],
                        ConfigurationManager.AppSettings["Inventec.YttConsumer.LoginName"],
                        ConfigurationManager.AppSettings["Inventec.YttConsumer.Password"]);
                    yttConsumer.UseRegistry(false);
                }
                return yttConsumer;
            }
        }

        private static ApiConsumerWrapper acsConsumerWrapper;
        public static ApiConsumerWrapper AcsConsumerWrapper
        {
            get
            {
                if (acsConsumerWrapper == null)
                {
                    acsConsumerWrapper = new ApiConsumerWrapper(
                        true,
                        MOS.UTILITY.Constant.APPLICATION_CODE,
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.Acs.Uri"],
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.Acs.Uri"],
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.LoginName"],
                        ConfigurationManager.AppSettings["Inventec.EmrConsumer.Password"]);
                    acsConsumerWrapper.UseRegistry(false);
                }
                return acsConsumerWrapper;
            }
        }

        private static ApiConsumerWrapper nmsConsumer;
        public static ApiConsumerWrapper NmsConsumer
        {
            get
            {
                if (nmsConsumer == null)
                {
                    nmsConsumer = new ApiConsumerWrapper(
                        true,
                        MOS.UTILITY.Constant.APPLICATION_CODE,
                        ConfigurationManager.AppSettings["Inventec.NmsConsumer.Base.Uri"],
                        ConfigurationManager.AppSettings["Inventec.NmsConsumer.Acs.Uri"],
                        ConfigurationManager.AppSettings["Inventec.NmsConsumer.LoginName"],
                        ConfigurationManager.AppSettings["Inventec.NmsConsumer.Password"]);
                    nmsConsumer.UseRegistry(false);
                }
                return nmsConsumer;
            }
        }

        public static ApiConsumerWrapper hxtConsumerWrapper { get; set; }
    }
}
