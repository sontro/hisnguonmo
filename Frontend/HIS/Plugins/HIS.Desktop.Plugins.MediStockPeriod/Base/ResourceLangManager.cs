using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockPeriod.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCMediStockPeriod { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCMediStockPeriod = new ResourceManager("HIS.Desktop.Plugins.MediStockPeriod.Resources.Lang", typeof(HIS.Desktop.Plugins.MediStockPeriod.UCMediStockPeriod).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
