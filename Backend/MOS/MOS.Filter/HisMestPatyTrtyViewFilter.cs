
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMestPatyTrtyViewFilter : FilterBase
    {

        public long? MEDI_STOCK_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public long? TREATMENT_TYPE_ID { get; set; }


        public List<long> MEDI_STOCK_IDs { get; set; }
        public List<long> PATIENT_TYPE_IDs { get; set; }
        public List<long> TREATMENT_TYPE_IDs { get; set; }


        public long? ID__NOT_EQUAL { get; set; }


        public HisMestPatyTrtyViewFilter()
            : base()
        {
        }
    }
}
