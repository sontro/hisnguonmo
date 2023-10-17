using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTransactionList { get; set; }
        internal static ResourceManager LanguageFrmCancelTransaction { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTransactionList = new ResourceManager("HIS.Desktop.Plugins.TransactionList.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionList.frmTransactionList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            try
            {
                //LanguageFrmCancelTransaction = new ResourceManager("HIS.Desktop.Plugins.TransactionList.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionList.frmCancelTransaction).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
