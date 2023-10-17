
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAntigenMetyFilter : FilterBase
    {
        public long? MEDICINE_TYPE_ID { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }

        public HisAntigenMetyFilter()
            : base()
        {
        }
    }
}
