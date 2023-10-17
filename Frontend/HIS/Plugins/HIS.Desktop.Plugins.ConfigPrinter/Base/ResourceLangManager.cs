using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfigPrinter.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmConfigPrinter { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmConfigPrinter = new ResourceManager("HIS.Desktop.Plugins.ConfigPrinter.Resources.Lang", typeof(HIS.Desktop.Plugins.ConfigPrinter.frmConfigPrinter).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
