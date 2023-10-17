using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterV2.Config
{
    class BHXHLoginCFG
    {
        private const string CONFIG_KEY = "HIS.CHECK_HEIN_CARD.BHXH.LOGIN.USER_PASS";

        private static string username;
        public static string USERNAME
        {
            get
            {
                if (String.IsNullOrEmpty(username))
                {
                    username = Get(HisConfigs.Get<string>(CONFIG_KEY), 0);
                }
                return username;
            }
            set { username = value; }
        }

        private static string password;
        public static string PASSWORD
        {
            get
            {
                if (String.IsNullOrEmpty(password))
                {
                    password = Get(HisConfigs.Get<string>(CONFIG_KEY), 1);
                }
                return password;
            }
            set { password = value; }
        }

        private static string Get(string value, int index)
        {
            string user = "";
            try
            {
                if (!String.IsNullOrEmpty(value))
                {
                    var data = value.Split(':');
                    if (data != null && data.Length >= index)
                    {
                        user = data[index];
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                user = "";
            }
            return user;
        }
    }
}
