using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrHospitalFees.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmAggrHospitalFees { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmAggrHospitalFees = new ResourceManager("HIS.Desktop.Plugins.AggrHospitalFees.Resources.Lang", typeof(HIS.Desktop.Plugins.AggrHospitalFees.frmAggrHospitalFees).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
