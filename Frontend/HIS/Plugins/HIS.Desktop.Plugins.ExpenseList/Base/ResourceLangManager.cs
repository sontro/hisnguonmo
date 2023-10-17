using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpenseList { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpenseList = new ResourceManager("HIS.Desktop.Plugins.ExpenseList.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpenseList.UCExpenseList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
