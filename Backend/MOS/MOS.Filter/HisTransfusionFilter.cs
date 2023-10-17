
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTransfusionFilter : FilterBase
    {
        public long? TRANSFUSION_SUM_ID { get; set; }


        public List<long> TRANSFUSION_SUM_IDs { get; set; }

        public long? MEASURE_TIME_FROM { get; set; }
        public long? MEASURE_TIME_TO { get; set; }

        public HisTransfusionFilter()
            : base()
        {
        }
    }
}
