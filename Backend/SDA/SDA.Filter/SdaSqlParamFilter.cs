using System.Collections.Generic;

namespace SDA.Filter
{
    public class SdaSqlParamFilter : FilterBase
    {
        public long? SQL_ID { get; set; }

        public List<long> SQL_IDs { get; set; }
        public SdaSqlParamFilter()
            : base()
        {
        }
    }
}
