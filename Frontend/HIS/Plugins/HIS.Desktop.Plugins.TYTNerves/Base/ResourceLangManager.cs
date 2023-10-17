using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace TYT.Desktop.Plugins.Nerves.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTYTNerves { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTYTNerves = new ResourceManager("TYT.Desktop.Plugins.Nerves.Resources.Lang", typeof(TYT.Desktop.Plugins.Nerves.frmTYTNerves).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
