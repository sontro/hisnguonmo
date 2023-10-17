using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseByPeriod.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpenseByPeriod { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpenseByPeriod = new ResourceManager("HIS.Desktop.Plugins.ExpenseByPeriod.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpenseByPeriod.UCExpenseByPeriod).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
