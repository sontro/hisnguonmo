
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAdrViewFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public HisAdrViewFilter()
            : base()
        {
        }
    }
}
