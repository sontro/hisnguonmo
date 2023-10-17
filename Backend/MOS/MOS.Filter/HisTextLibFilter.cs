
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTextLibFilter : FilterBase
    {
        public string HASHTAG { get; set; }
        public List<string> HASHTAGs { get; set; }
        public bool? CAN_VIEW { get; set; }
        public string HOT_KEY__EXACT { get; set; }
        public bool? IS_PUBLIC { get; set; }
        public long? ID_NOT__EQUAL { get; set; }
        public bool? HAS_HOT_KEY { get; set; }
        public long? PUBLIC_DEPARTMENT_ID { get; set; }
        public long? LIB_TYPE_ID { get; set; }

        public HisTextLibFilter()
            : base()
        {
        }
    }
}
