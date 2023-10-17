using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SwapService.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCSwapService { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCSwapService = new ResourceManager("HIS.Desktop.Plugins.SwapService.Resources.Lang", typeof(HIS.Desktop.Plugins.SwapService.frmSwapService).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
