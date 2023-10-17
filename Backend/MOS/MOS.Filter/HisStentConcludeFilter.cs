
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisStentConcludeFilter : FilterBase
    {
        public long? SERE_SERV_ID { get; set; }
        public List<long> SERE_SERV_IDs { get; set; }
        public HisStentConcludeFilter()
            : base()
        {
        }
    }
}
