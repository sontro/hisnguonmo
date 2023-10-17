
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineFilter : FilterBase
    {
        public long? MEDICINE_TYPE_ID { get; set; }
        public long? SUPPLIER_ID { get; set; }
        public long? BID_ID { get; set; }
        public long? MEDICAL_CONTRACT_ID { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }

        public string TDL_IMP_MEST_CODE { get; set; }
        public string TDL_IMP_MEST_SUB_CODE { get; set; }
        
        public HisMedicineFilter()
            : base()
        {
        }
    }
}
