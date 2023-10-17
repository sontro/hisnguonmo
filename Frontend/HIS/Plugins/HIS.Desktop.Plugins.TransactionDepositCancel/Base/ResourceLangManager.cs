using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDepositCancel.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTransactionDepositCancel { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTransactionDepositCancel = new ResourceManager("HIS.Desktop.Plugins.TransactionDepositCancel.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionDepositCancel.frmTransactionDepositCancel).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
