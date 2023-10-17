using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediReactSum.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmMediReactSum { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmMediReactSum = new ResourceManager("HIS.Desktop.Plugins.MediReactSum.Resources.Lang", typeof(HIS.Desktop.Plugins.MediReactSum.frmMediReactSum).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
