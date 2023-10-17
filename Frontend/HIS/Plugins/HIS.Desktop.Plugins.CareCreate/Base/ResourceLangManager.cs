using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CareCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageCareCreate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageCareCreate = new ResourceManager("HIS.Desktop.Plugins.CareCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.CareCreate.CareCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
