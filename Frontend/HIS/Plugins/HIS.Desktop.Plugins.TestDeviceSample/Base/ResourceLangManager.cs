using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestDeviceSample.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTestDeviceSample { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTestDeviceSample = new ResourceManager("HIS.Desktop.Plugins.TestDeviceSample.Resources.Lang", typeof(HIS.Desktop.Plugins.TestDeviceSample.frmTestDeviceSample).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
