using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportBlood.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmExpMestBlood { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmExpMestBlood = new ResourceManager("HIS.Desktop.Plugins.ExportBlood.Resources.Lang", typeof(HIS.Desktop.Plugins.ExportBlood.frmExpMestBlood).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
