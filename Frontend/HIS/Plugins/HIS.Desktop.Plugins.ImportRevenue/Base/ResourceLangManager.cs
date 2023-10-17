using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportRevenue.Base
{
    internal class ResourceLangManager
    {
        internal static ResourceManager LanguageUCImportRevenue { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCImportRevenue = new ResourceManager("HIS.Desktop.Plugins.ImportRevenue.Resources.Lang", typeof(HIS.Desktop.Plugins.ImportRevenue.UCImportRevenue).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
