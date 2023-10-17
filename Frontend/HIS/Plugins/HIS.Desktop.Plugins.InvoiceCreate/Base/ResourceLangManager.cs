using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmInvoiceCreate { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmInvoiceCreate = new ResourceManager("HIS.Desktop.Plugins.InvoiceCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.InvoiceCreate.frmInvoiceCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
