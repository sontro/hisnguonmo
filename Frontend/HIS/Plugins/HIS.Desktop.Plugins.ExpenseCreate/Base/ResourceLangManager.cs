using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpenseCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmExpenseCreate { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmExpenseCreate = new ResourceManager("HIS.Desktop.Plugins.ExpenseCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpenseCreate.frmExpenseCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
