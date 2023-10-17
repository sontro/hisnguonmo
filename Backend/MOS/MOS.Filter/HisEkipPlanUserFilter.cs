
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisEkipPlanUserFilter : FilterBase
    {
        public long? EKIP_PLAN_ID { get; set; }
        public List<long> EKIP_PLAN_IDs { get; set; }

        public HisEkipPlanUserFilter()
            : base()
        {
        }
    }
}
