
using System.Collections.Generic;
namespace SDA.Filter
{
    public class SdaNotifyFilter : FilterBase
    {
        public List<string> LOGIN_NAMES { get; set; }

        public long? FROM_TIME { get; set; }
        public long? TO_TIME { get; set; }
        public long? NOW_TIME { get; set; }

        public string RECEIVER_LOGINNAME_EXACT_OR_NULL { get; set; }

        public bool? HAS_RECEIVER_LOGINNAME_OR_NULL { get; set; }
        public bool? WATCHED { get; set; }
        public string RECEIVER_DEPARTMENT_CODES_OR_NULL { get; set; }
        public string RECEIVER_LOGINNAMES_EXACT_OR_NULL { get; set; }

        public SdaNotifyFilter()
            : base()
        {
        }
    }
}
