using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PeriodList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCPeriodList { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCPeriodList = new ResourceManager("HIS.Desktop.Plugins.PeriodList.Resources.Lang", typeof(HIS.Desktop.Plugins.PeriodList.UCPeriodList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
