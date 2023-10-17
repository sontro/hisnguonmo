
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentFileFilter : FilterBase
    {
        public long? FILE_TYPE_ID { get; set; }
        public long? TREATMENT_ID { get; set; }

        public List<long> FILE_TYPE_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public HisTreatmentFileFilter()
            : base()
        {
        }
    }
}
