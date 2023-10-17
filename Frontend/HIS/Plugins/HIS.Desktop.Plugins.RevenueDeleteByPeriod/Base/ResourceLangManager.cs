using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RevenueDeleteByPeriod.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmRevenueDeleteByPeriod { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmRevenueDeleteByPeriod = new ResourceManager("HIS.Desktop.Plugins.RevenueDeleteByPeriod.Resources.Lang", typeof(HIS.Desktop.Plugins.RevenueDeleteByPeriod.frmRevenueDeleteByPeriod).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
