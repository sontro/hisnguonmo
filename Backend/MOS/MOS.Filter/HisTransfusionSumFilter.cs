
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTransfusionSumFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? ROOM_ID { get; set; }
        public long? EXP_MEST_BLOOD_ID { get; set; }


        public List<long> TREATMENT_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<long> ROOM_IDs { get; set; }
        public List<long> EXP_MEST_BLOOD_IDs { get; set; }

        public HisTransfusionSumFilter()
            : base()
        {
        }
    }
}
