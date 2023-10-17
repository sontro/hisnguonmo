
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMediContractMatyViewFilter : FilterBase
    {

        public long? MEDICAL_CONTRACT_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }
        public long? BID_MATERIAL_TYPE_ID { get; set; }


        public List<long> MEDICAL_CONTRACT_IDs { get; set; }
        public List<long> MATERIAL_TYPE_IDs { get; set; }
        public List<long> BID_MATERIAL_TYPE_IDs { get; set; }

        public HisMediContractMatyViewFilter()
            : base()
        {
        }
    }
}
