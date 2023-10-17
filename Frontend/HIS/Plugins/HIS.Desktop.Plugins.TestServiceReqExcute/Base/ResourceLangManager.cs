using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestServiceReqExcute.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTestServiceReqExcute { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTestServiceReqExcute = new ResourceManager("HIS.Desktop.Plugins.TestServiceReqExcute.Resources.Lang", typeof(HIS.Desktop.Plugins.TestServiceReqExcute.UCTestServiceReqExcute).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
