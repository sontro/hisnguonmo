
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPrepareMetyFilter : FilterBase
    {
        public long? PREPARE_ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }

        public List<long> PREPARE_IDs { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> TDL_TREATMENT_IDs { get; set; }

        public long? PREPARE_ID__NOT_EQUAL { get; set; }

        public bool? HAS_APPROVAL_AMOUNT { get; set; }

        public HisPrepareMetyFilter()
            : base()
        {
        }
    }
}
