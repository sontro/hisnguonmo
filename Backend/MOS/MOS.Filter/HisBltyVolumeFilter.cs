
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBltyVolumeFilter : FilterBase
    {
        public long? BLOOD_VOLUME_ID { get; set; }
        public long? BLOOD_TYPE_ID { get; set; }

        public List<long> BLOOD_VOLUME_IDs { get; set; }
        public List<long> BLOOD_TYPE_IDs { get; set; }

        public HisBltyVolumeFilter()
            : base()
        {
        }
    }
}
