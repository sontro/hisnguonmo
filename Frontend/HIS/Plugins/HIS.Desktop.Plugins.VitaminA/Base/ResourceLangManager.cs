using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.VitaminA.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCVitaminA { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                //LanguageUCVitaminA = new ResourceManager("HIS.Desktop.Plugins.VitaminA.Resources.Lang", typeof(HIS.Desktop.Plugins.VitaminA.frmVitaminA).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
