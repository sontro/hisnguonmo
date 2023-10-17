using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportBlood.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCImportBlood { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCImportBlood = new ResourceManager("HIS.Desktop.Plugins.ImportBlood.Resources.Lang", typeof(HIS.Desktop.Plugins.ImportBlood.UCImportBloodPlus).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
