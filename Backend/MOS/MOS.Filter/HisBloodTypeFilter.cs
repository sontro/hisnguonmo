
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBloodTypeFilter : FilterBase
    {
        public long? PARENT_ID { get; set; }
        public List<long> PARENT_IDs { get; set; }

        public long? BLOOD_VOLUME_ID { get; set; }
        public List<long> BLOOD_VOLUME_IDs { get; set; }

        public HisBloodTypeFilter()
            : base()
        {
        }
    }
}
