
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceHeinViewFilter : FilterBase
    {
        public string SERVICE_CODE__EXACT { get; set; }
        public string SERVICE_TYPE_CODE__EXACT { get; set; }
        public string BRANCH_CODE__EXACT { get; set; }

        public long? SERVICE_ID { get; set; }
        public long? BRANCH_ID { get; set; }
        public long? SERVICE_TYPE_ID { get; set; }

        public List<long> SERVICE_IDs { get; set; }
        public List<long> BRANCH_IDs { get; set; }
        public List<long> SERVICE_TYPE_IDs { get; set; }
        
        public HisServiceHeinViewFilter()
            : base()
        {
        }
    }
}
