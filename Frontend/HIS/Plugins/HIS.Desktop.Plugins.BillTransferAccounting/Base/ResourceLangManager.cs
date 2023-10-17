using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BillTransferAccounting.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmBillTransferAccounting { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmBillTransferAccounting = new ResourceManager("HIS.Desktop.Plugins.BillTransferAccounting.Resources.Lang", typeof(HIS.Desktop.Plugins.BillTransferAccounting.frmBillTransferAccounting).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
