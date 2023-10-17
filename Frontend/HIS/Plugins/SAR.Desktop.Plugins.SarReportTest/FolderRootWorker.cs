using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarReportTest
{
    public class FolderRootWorker
    {
        private const string SOFTWARE_FOLDER = "SOFTWARE";
        private const string COMPANY_FOLDER = "INVENTEC";
        private static readonly string APP_FOLDER = ConfigurationManager.AppSettings["Inventec.Desktop.ApplicationCode"];

        public const string FOLDER_KEY = "SarReportAutoUnitTest";

        private static RegistryKey appFolder = Registry.CurrentUser.CreateSubKey(SOFTWARE_FOLDER).CreateSubKey(COMPANY_FOLDER).CreateSubKey(APP_FOLDER);

        public static void ChangeFolder(string fd)
        {
            try
            {
                appFolder.SetValue(FOLDER_KEY, fd);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public static string GetFolder()
        {
            string fd = "";
            try
            {
                var f = appFolder.GetValue(FOLDER_KEY, "");
                fd = (f ?? "").ToString();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return fd;
        }

    }
}
