using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Camera
{
    public class ApplicationCaptureTypeWorker
    {
        private static RegistryKey appFolder = Registry.CurrentUser.CreateSubKey(RegistryConstant.SOFTWARE_FOLDER).CreateSubKey(RegistryConstant.COMPANY_FOLDER).CreateSubKey(RegistryConstant.APP_FOLDER);

        public static void ChangeCaptureConnectType(int CaptureConnectType)
        {
            try
            {
                appFolder.SetValue(RegistryConstant.CONNECTION_TYPE_KEY, CaptureConnectType);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public static int GetCaptureConnectType()
        {
            int captureConnectType = 1;
            try
            {
                var f = appFolder.GetValue(RegistryConstant.CONNECTION_TYPE_KEY, 1);
                captureConnectType = Inventec.Common.TypeConvert.Parse.ToInt32(f.ToString());
            }
            catch (Exception ex)
            {
                captureConnectType = 1;
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return captureConnectType;
        }               
    }
}
