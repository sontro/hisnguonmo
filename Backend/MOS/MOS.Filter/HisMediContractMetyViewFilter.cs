
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMediContractMetyViewFilter : FilterBase
    {
        public long? MEDICAL_CONTRACT_ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }
        public long? BID_MEDICINE_TYPE_ID { get; set; }


        public List<long> MEDICAL_CONTRACT_IDs { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> BID_MEDICINE_TYPE_IDs { get; set; }

        public HisMediContractMetyViewFilter()
            : base()
        {
        }
    }
}
