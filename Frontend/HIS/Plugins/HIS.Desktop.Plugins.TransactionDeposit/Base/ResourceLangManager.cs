using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDeposit.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTransactionDeposit { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTransactionDeposit = new ResourceManager("HIS.Desktop.Plugins.TransactionDeposit.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionDeposit.frmTransactionDeposit).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
