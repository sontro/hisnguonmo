using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXml.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExportXml { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExportXml = new ResourceManager("HIS.Desktop.Plugins.ExportXml.Resources.Lang", typeof(HIS.Desktop.Plugins.ExportXml.UCExportXml).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
