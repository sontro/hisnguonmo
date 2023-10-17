
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisSurgRemunerationFilter : FilterBase
    {
        public long? ID_NOT_EQUAL { get; set; }
        public long? PTTT_GROUP_ID { get; set; }
        public long? EMOTIONLESS_METHOD_ID { get; set; }
        public long? SERVICE_TYPE_ID { get; set; }

        public List<long> PTTT_GROUP_IDs { get; set; }
        public List<long> EMOTIONLESS_METHOD_IDs { get; set; }
        public List<long> SERVICE_TYPE_IDs { get; set; }

        public HisSurgRemunerationFilter()
            : base()
        {
        }
    }
}
