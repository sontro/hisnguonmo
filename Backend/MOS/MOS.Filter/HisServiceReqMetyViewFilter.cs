
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceReqMetyViewFilter : FilterBase
    {
        public long? SERVICE_REQ_ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }
        public List<long> SERVICE_REQ_IDs { get; set; }
        public List<long> SERVICE_REQ_ID__NOT_INs { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public long? INTRUCTION_DATE_FROM { get; set; }
        public long? INTRUCTION_DATE_TO { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? INTRUCTION_DATE__EQUAL { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public HisServiceReqMetyViewFilter()
            : base()
        {
        }
    }
}
