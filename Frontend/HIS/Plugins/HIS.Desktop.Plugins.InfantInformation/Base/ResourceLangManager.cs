using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InfantInformation.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmInfantInformation { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmInfantInformation = new ResourceManager("HIS.Desktop.Plugins.InfantInformation.Resources.Lang", typeof(HIS.Desktop.Plugins.InfantInformation.frmInfantInformation).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
