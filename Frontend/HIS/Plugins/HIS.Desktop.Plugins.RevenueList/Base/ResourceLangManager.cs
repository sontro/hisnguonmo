using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RevenueList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCRevenueList { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCRevenueList = new ResourceManager("HIS.Desktop.Plugins.RevenueList.Resources.Lang", typeof(HIS.Desktop.Plugins.RevenueList.UCRevenueList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
