using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineSaleBill.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguagefrmMedicineSaleBill { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguagefrmMedicineSaleBill = new ResourceManager("HIS.Desktop.Plugins.MedicineSaleBill.Resources.Lang", typeof(HIS.Desktop.Plugins.MedicineSaleBill.frmMedicineSaleBill).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
