using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockSummaryByExpireDate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCMediStockSummaryByExpireDate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCMediStockSummaryByExpireDate = new ResourceManager("HIS.Desktop.Plugins.MediStockSummaryByExpireDate.Resources.Lang", typeof(HIS.Desktop.Plugins.MediStockSummaryByExpireDate.UCMediStockSummaryByExpireDate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
