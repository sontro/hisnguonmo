
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineViewFilter : FilterBase
    {
        public string BID_NUMBER__EXACT { get; set; }
        public long? BID_ID { get; set; }
        public List<long> BID_IDs { get; set; }

        public string TDL_IMP_MEST_CODE { get; set; }
        public string TDL_IMP_MEST_SUB_CODE { get; set; }
        public string MEDICINE_TYPE_CODE { get; set; }
        public string PACKAGE_NUMBER { get; set; }
        public string SUPPLIER_CODE { get; set; }

        public HisMedicineViewFilter()
            : base()
        {
        }
    }
}
