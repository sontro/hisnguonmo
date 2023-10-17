using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InfusionSumByTreatment.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmInfusionSumByTreatment { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmInfusionSumByTreatment = new ResourceManager("HIS.Desktop.Plugins.InfusionSumByTreatment.Resources.Lang", typeof(HIS.Desktop.Plugins.InfusionSumByTreatment.frmInfusionSumByTreatment).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
