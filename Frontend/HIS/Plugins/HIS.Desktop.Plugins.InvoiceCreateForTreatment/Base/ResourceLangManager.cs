using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InvoiceCreateForTreatment.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmInvoiceCreateForTreatment { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmInvoiceCreateForTreatment = new ResourceManager("HIS.Desktop.Plugins.InvoiceCreateForTreatment.Resources.Lang", typeof(HIS.Desktop.Plugins.InvoiceCreateForTreatment.frmInvoiceCreateForTreatment).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
