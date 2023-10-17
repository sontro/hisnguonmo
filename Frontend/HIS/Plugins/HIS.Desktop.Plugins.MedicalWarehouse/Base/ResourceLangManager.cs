using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalWarehouse.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCMedicalWarehouse { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCMedicalWarehouse = new ResourceManager("HIS.Desktop.Plugins.MedicalWarehouse.Resources.Lang", typeof(HIS.Desktop.Plugins.MedicalWarehouse.UCMedicalWarehouse).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
