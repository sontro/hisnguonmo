using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.NervesList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTYTNervesList { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTYTNervesList = new ResourceManager("TYT.Desktop.Plugins.NervesList.Resources.Lang", typeof(TYT.Desktop.Plugins.NervesList.UCTYTNervesList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
