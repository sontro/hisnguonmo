
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceFilter : FilterBase
    {
        public long? SERVICE_TYPE_ID { get; set; }
        public long? SERVICE_UNIT_ID { get; set; }
        public long? ICD_CM_ID { get; set; }
        public long? PTTT_GROUP_ID { get; set; }
        public List<long> PTTT_GROUP_IDs { get; set; }
        public List<long> ICD_CM_IDs { get; set; }
        public long? ID_NOT__EQUAL { get; set; }
        public string SERVICE_CODE__EXACT { get; set; }
        public long? PARENT_ID { get; set; }
        public long? RATION_GROUP_ID { get; set; }
        public long? PACKAGE_ID { get; set; }
        public short? MUST_BE_CONSULTED { get; set; }
        
        public HisServiceFilter()
            : base()
        {
        }
    }
}
