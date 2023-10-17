using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionCancel.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTransactionDepositCancel { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTransactionDepositCancel = new ResourceManager("HIS.Desktop.Plugins.TransactionCancel.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionCancel.frmTransactionCancel).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
