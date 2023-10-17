using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineVaccinBill.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguagefrmMedicineVaccinBill { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguagefrmMedicineVaccinBill = new ResourceManager("HIS.Desktop.Plugins.MedicineVaccinBill.Resources.Lang", typeof(HIS.Desktop.Plugins.MedicineVaccinBill.frmMedicineVaccinBill).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
