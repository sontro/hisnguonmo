using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillOther.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LangFrmTransactionBillOther { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LangFrmTransactionBillOther = new ResourceManager("HIS.Desktop.Plugins.TransactionBillOther.Resources.Lang", typeof(HIS.Desktop.Plugins.TransactionBillOther.frmTransactionBillOther).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
