using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Utility
{
    public class RegistrySettingWorker
    {
        const string SOFTWARE_FOLDER = "SOFTWARE";
        const string COMPANY_FOLDER = "INVENTEC";
        static readonly string APP_FOLDER = ConfigurationManager.AppSettings["Inventec.Desktop.ApplicationCode"];

        public const string FONT_KEY = "FontName";
        private static RegistryKey appFolder = Registry.CurrentUser.CreateSubKey(SOFTWARE_FOLDER).CreateSubKey(COMPANY_FOLDER).CreateSubKey(APP_FOLDER);

        public static void ChangeValue(string key, object value)
        {
            try
            {              
                appFolder.SetValue(key, value);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public static object GetValue(string key)
        {
            object value = null;
            try
            {
                var f = appFolder.GetValue(key);
                value = f;
            }
            catch (Exception ex)
            {               
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return value;
        }
    }
}
