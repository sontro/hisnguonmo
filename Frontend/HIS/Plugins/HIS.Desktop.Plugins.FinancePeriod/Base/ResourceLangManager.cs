using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.FinancePeriod.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTYTFinancePeriod { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTYTFinancePeriod = new ResourceManager("HIS.Desktop.Plugins.FinancePeriod.Resources.Lang", typeof(HIS.Desktop.Plugins.FinancePeriod.UCFinancePeriod).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
