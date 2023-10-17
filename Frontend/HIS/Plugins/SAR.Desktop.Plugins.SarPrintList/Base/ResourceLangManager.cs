using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarPrintList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmPrintList { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmPrintList = new ResourceManager("SAR.Desktop.Plugins.SarPrintList.Resources.Lang", typeof(SAR.Desktop.Plugins.SarPrintList.frmSarPrintList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
