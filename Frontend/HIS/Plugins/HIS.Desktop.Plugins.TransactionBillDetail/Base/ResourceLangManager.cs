using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillDetail.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTransactionBillDetail { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTransactionBillDetail = new ResourceManager("HIS.Desktop.Plugins.TransactionBillDetail.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionBillDetail.frmTransactionBillDetail).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
