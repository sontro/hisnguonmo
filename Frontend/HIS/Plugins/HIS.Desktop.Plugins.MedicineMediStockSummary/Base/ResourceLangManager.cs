using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineMediStockSummary.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCMediStockSummary { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCMediStockSummary = new ResourceManager("HIS.Desktop.Plugins.MedicineMediStockSummary.Resources.Lang", typeof(HIS.Desktop.Plugins.MedicineMediStockSummary.ucMediaStockSummaryByMedicine).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
