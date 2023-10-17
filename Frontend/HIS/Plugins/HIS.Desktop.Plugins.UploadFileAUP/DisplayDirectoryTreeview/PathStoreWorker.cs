using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UploadFileAUP
{
    internal class PathStoreWorker
    {
        private static RegistryKey appFolder = Registry.CurrentUser.CreateSubKey(RegistryConstant.SOFTWARE_FOLDER).CreateSubKey(RegistryConstant.COMPANY_FOLDER).CreateSubKey(RegistryConstant.APP_FOLDER);

        internal static void ChangeWorkingPath(string path)
        {
            try
            {
                appFolder.SetValue(RegistryConstant.DATA_KEY, path);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        internal static string GetWorkingPath()
        {
            string path = "";
            try
            {
                var f = appFolder.GetValue(RegistryConstant.DATA_KEY, "");
                path = f != null ? f.ToString() : "";
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return path;
        }
    }
}
