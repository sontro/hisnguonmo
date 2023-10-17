
using System.Collections.Generic;
namespace SDA.Filter
{
    public class SdaDeleteDataFilter : FilterBase
    {
        public long? DATA_ID { get; set; }
        public List<long> DATA_IDs { get; set; }

        public SdaDeleteDataFilter()
            : base()
        {
        }
    }
}
