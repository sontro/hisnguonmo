using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionRepay.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTransactionRepay { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTransactionRepay = new ResourceManager("HIS.Desktop.Plugins.TransactionRepay.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionRepay.frmTransactionRepay).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
