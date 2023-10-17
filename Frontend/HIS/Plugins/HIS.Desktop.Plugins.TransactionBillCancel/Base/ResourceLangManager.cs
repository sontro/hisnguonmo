using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillCancel.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTransactionBillCancel { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTransactionBillCancel = new ResourceManager("HIS.Desktop.Plugins.TransactionBillCancel.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionBillCancel.frmTransactionBillCancel).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
