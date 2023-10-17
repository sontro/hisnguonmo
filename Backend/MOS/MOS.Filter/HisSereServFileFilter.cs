
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisSereServFileFilter : FilterBase
    {
        public long? SERE_SERV_ID { get; set; }
        public List<long> SERE_SERV_IDs { get; set; }
        public long? BODY_PART_ID { get; set; }
        public List<long> BODY_PART_IDs { get; set; }

        public HisSereServFileFilter()
            : base()
        {
        }
    }
}
