using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InsuranceExpertise.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCInsuranceExpertise { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCInsuranceExpertise = new ResourceManager("HIS.Desktop.Plugins.InsuranceExpertise.Resources.Lang", typeof(HIS.Desktop.Plugins.InsuranceExpertise.UCInsuranceExpertise).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
