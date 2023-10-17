using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class HisDispenseCFG
    {

        private const string CFG_MEDICINE_IMP_PRICE_OPTION = "MOS.HIS_DISPENSE.MEDICINE_IMP_PRICE_OPTION";

        private static bool? isMedicineImpPriceOption;
        public static bool IS_MEDICINE_IMP_PRICE_OPTION
        {
            get
            {
                if (!isMedicineImpPriceOption.HasValue)
                {
                    isMedicineImpPriceOption = ConfigUtil.GetIntConfig(CFG_MEDICINE_IMP_PRICE_OPTION) == 1;
                }
                return isMedicineImpPriceOption.Value;
            }
            set
            {
                isMedicineImpPriceOption = value;
            }
        }

        public static void Reload()
        {
            isMedicineImpPriceOption = ConfigUtil.GetIntConfig(CFG_MEDICINE_IMP_PRICE_OPTION) == 1;
        }
    }
}
