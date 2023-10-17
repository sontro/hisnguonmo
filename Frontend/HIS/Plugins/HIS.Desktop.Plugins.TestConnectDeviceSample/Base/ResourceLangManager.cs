using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestConnectDeviceSample.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTestConnectDeviceSample { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTestConnectDeviceSample = new ResourceManager("HIS.Desktop.Plugins.TestConnectDeviceSample.Resources.Lang", typeof(HIS.Desktop.Plugins.TestConnectDeviceSample.UCTestConnectDeviceSample).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
