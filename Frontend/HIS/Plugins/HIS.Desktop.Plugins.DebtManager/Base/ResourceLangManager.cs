using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DebtManager.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCDebtManager { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCDebtManager = new ResourceManager("HIS.Desktop.Plugins.DebtManager.Resources.Lang", typeof(HIS.Desktop.Plugins.DebtManager.UCDebtManager).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
