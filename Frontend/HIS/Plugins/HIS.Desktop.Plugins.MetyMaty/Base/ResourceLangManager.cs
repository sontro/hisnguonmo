using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MetyMaty.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmMetyMaty { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmMetyMaty = new ResourceManager("HIS.Desktop.Plugins.MetyMaty.Resources.Lang", typeof(HIS.Desktop.Plugins.MetyMaty.MetyMatyForm).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
