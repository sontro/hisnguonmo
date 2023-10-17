using HIS.Desktop.LocalStorage.ConfigApplication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatientVer7
{
    internal class CallPatientProcess
    {
        internal static string[] FileName, FilePath;
        public static void GetFilePath()
        {
            try
            {
                //FilePath = Directory.GetFiles(GlobalStore.DirectoryAdvertisement);
                if (!string.IsNullOrEmpty(ConfigApplicationWorker.Get<string>(AppConfigKeys.CONFIG_KEY__DUONG_DAN_CHAY_FILE_VIDEO)))
                {
                    FilePath = Directory.GetFiles(ConfigApplicationWorker.Get<string>(AppConfigKeys.CONFIG_KEY__DUONG_DAN_CHAY_FILE_VIDEO));
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }

        }
    }
}
