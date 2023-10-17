using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisKskOccupationalFilter : FilterBase
    {
        public long? SERVICE_REQ_ID { get; set; }

        public List<long> SERVICE_REQ_IDs { get; set; }

        public HisKskOccupationalFilter()
            : base()
        {
        }
    }
}
