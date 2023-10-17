using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineType
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCMedicineType { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCMedicineType = new ResourceManager("HIS.Desktop.Plugins.MedicineType.Resources.Lang", typeof(HIS.Desktop.Plugins.MedicineType.MedicineTypeList.UCMedcineTypeList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
