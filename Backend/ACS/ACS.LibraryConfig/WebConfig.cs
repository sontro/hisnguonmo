using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.LibraryConfig
{
    public class WebConfig
    {

        private static string CFG__IS_USING_SSO = (System.Configuration.ConfigurationSettings.AppSettings["Inventec.Token.AuthSystem.IsUsingSSO"] ?? "0");
        public static bool IS_USING_SSO
        {
            get
            {
                if (!String.IsNullOrEmpty(CFG__IS_USING_SSO))
                {
                    return (CFG__IS_USING_SSO == "1");
                }

                return false;
            }
        }

              
        private static string CFG__IS_MASTER = (System.Configuration.ConfigurationSettings.AppSettings["ACS.IS_MASTER"] ?? "1");
        public static bool IS_MASTER
        {
            get
            {
                if (!String.IsNullOrEmpty(CFG__IS_MASTER))
                {
                    return (CFG__IS_MASTER == "1");
                }

                return false;
            }
        }

        public static bool IS_SLAVE
        {
            get
            {
                if (!String.IsNullOrEmpty(CFG__IS_MASTER))
                {
                    return (CFG__IS_MASTER == "0");
                }

                return false;
            }
        }

        private static string CFG__BACKPLANE_ADDRESSES = (System.Configuration.ConfigurationSettings.AppSettings["ACS.BACKPLANE_ADDRESSES.URI"] ?? "");
        private static List<string> blackPlaneAddress;
        public static List<string> LIST_BACKPLANE_ADDRESSES
        {
            get
            {
                if (!String.IsNullOrEmpty(CFG__BACKPLANE_ADDRESSES))
                {
                    if (blackPlaneAddress == null)
                    {
                        blackPlaneAddress = CFG__BACKPLANE_ADDRESSES.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                }

                return blackPlaneAddress;
            }
        }

        public static bool IsUsingJWT = ((System.Configuration.ConfigurationSettings.AppSettings["Inventec.Token.AuthSystem.IsUsingJWT"] ?? "") == "1" ? true : false);
        public static string IsNotGetTkenFromDB = System.Configuration.ConfigurationSettings.AppSettings["Inventec.Token.AuthSystem.IsNotGetTkenFromDB"];
        public static string ACS__Authentication__IsUser = (System.Configuration.ConfigurationSettings.AppSettings["ACS.Authentication.IsUser"] ?? "");
        public static string URI_API_ACS = System.Configuration.ConfigurationSettings.AppSettings["Inventec.Token.ResourceSystem.AcsTokenGet.Uri.Base"];
        public static string URI_API_SDA = System.Configuration.ConfigurationSettings.AppSettings["Inventec.Token.Base.ApiConsumerStore.SdaConsumer.Uri"];
        public static bool IS_APPLICATION_GENERATE_PASSWORD = ((System.Configuration.ConfigurationSettings.AppSettings["ACS.ACS_USER.GeneratePass.Auto"] ?? "") == "1" ? true : false);
        public static string URI_API_SMS = System.Configuration.ConfigurationSettings.AppSettings["MANAGER.Base.ApiConsumerStore.Sms.Uri"];
        public static string INTEGRATED_SMS__MERCHANT_CODE = System.Configuration.ConfigurationSettings.AppSettings["MANAGER.Base.ApiConsumerStore.Sms.MerchantCode"];
        public static string INTEGRATED_SMS__SECURITY_CODE = System.Configuration.ConfigurationSettings.AppSettings["MANAGER.Base.ApiConsumerStore.Sms.SecurityCode"];
        public static bool IsInitTokenInRamForStartApp = ((System.Configuration.ConfigurationSettings.AppSettings["ACS.API.InitTokenInRamForStartApp"] ?? "") == "1" ? true : false);
        public static int AuthenRequest__Timeout = int.Parse((System.Configuration.ConfigurationSettings.AppSettings["ACS.AcsAuthenRequest.OtpAuthenRequest.Timeout"] ?? "5"));

        public static int AuthSystem__TokenTimeout = int.Parse((System.Configuration.ConfigurationSettings.AppSettings["Inventec.Token.AuthSystem.TokenTimeout"] ?? "10080"));
        public static string MailServerGmail__User = (System.Configuration.ConfigurationSettings.AppSettings["ACS.MANAGER.MailServerGmail.User"] ?? "integratemanagementsystem@gmail.com").ToString();
        public static string MailServerGmail__Password = (System.Configuration.ConfigurationSettings.AppSettings["ACS.MANAGER.MailServerGmail.Password"] ?? "Khoakn1985").ToString();
        public static string MailServerGmail__Body = (System.Configuration.ConfigurationSettings.AppSettings["ACS.MANAGER.MailServerGmail.Body"] ?? "Hệ thống xin thông báo mật khẩu mới của tài khoản {0} là: {1}{2}Vui lòng đăng nhập hệ thống và thực hiện đổi mật khẩu để đảm bảo an toàn thông tin.").ToString();
    }
}
