using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExportXmlQD4210 { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExportXmlQD4210 = new ResourceManager("HIS.Desktop.Plugins.ExportXmlQD4210.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpXml4210ObOtherBHYT.UCExportXml).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
