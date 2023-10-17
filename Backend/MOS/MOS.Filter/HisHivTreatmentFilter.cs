
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisHivTreatmentFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public short? IS_DELETE { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public HisHivTreatmentFilter()
            : base()
        {
        }
    }
}
