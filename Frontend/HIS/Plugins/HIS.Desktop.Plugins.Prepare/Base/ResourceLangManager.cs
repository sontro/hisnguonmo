using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Prepare.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmBorderau { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmBorderau = new ResourceManager("HIS.Desktop.Plugins.Prepare.Resources.Lang", typeof(HIS.Desktop.Plugins.Prepare.frmPrepare).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
