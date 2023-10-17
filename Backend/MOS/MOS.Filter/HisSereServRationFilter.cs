
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisSereServRationFilter : FilterBase
    {
        public long? SERVICE_ID { get; set; }
        public long? SERVICE_REQ_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }

        public List<long> SERVICE_IDs { get; set; }
        public List<long> SERVICE_REQ_IDs { get; set; }
        public List<long> PATIENT_TYPE_IDs { get; set; }

        public HisSereServRationFilter()
            : base()
        {
        }
    }
}
