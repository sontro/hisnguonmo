using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicineTypeAcin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class HisMedicineTypeAcinCFG
    {
        private static bool approvalRequiredLoaded;
        private static List<V_HIS_MEDICINE_TYPE_ACIN> approvalRequired;
        public static List<V_HIS_MEDICINE_TYPE_ACIN> APPROVAL_REQUIRED
        {
            get
            {
                if (!approvalRequiredLoaded)
                {
                    HisMedicineTypeAcinViewFilterQuery filter = new HisMedicineTypeAcinViewFilterQuery();
                    filter.IS_APPROVAL_REQUIRED = true;
                    approvalRequired = new HisMedicineTypeAcinGet().GetView(filter);
                    approvalRequiredLoaded = true;
                }
                return approvalRequired;
            }
        }

        private static bool approvalRequiredMedicineTypeIdLoaded;
        private static List<long> approvalRequiredMedicineTypeIds;
        public static List<long> APPROVAL_REQUIRED_MEDICINE_TYPE_IDS
        {
            get
            {
                if (!approvalRequiredMedicineTypeIdLoaded)
                {
                    approvalRequiredMedicineTypeIds = APPROVAL_REQUIRED != null ? APPROVAL_REQUIRED.Select(o => o.MEDICINE_TYPE_ID).Distinct().ToList() : null;
                    approvalRequiredMedicineTypeIdLoaded = true;
                }
                return approvalRequiredMedicineTypeIds;
            }
        }

        public static void Reload()
        {
            HisMedicineTypeAcinViewFilterQuery filter = new HisMedicineTypeAcinViewFilterQuery();
            filter.IS_APPROVAL_REQUIRED = true;
            approvalRequired = new HisMedicineTypeAcinGet().GetView(filter);
            approvalRequiredLoaded = true;

            approvalRequiredMedicineTypeIds = approvalRequired != null ? approvalRequired.Select(o => o.MEDICINE_TYPE_ID).Distinct().ToList() : null;
            approvalRequiredMedicineTypeIdLoaded = true;
        }
    }
}
