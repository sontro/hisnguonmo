
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisHoreHohaViewFilter : FilterBase
    {
        public long? HOLD_RETURN_ID { get; set; }
        public long? HORE_HANDOVER_ID { get; set; }
        public long? PATIENT_ID { get; set; }
        public List<long> HOLD_RETURN_IDs { get; set; }
        public List<long> HORE_HANDOVER_IDs { get; set; }
        public List<long> PATIENT_IDs { get; set; }
        

        public long? RETURN_TIME_FROM { get; set; }
        public long? RETURN_TIME_TO { get; set; }
        public long? HOLD_TIME_FROM { get; set; }
        public long? HOLD_TIME_TO { get; set; }

        public string HEIN_CARD_NUMBER__EXACT { get; set; }
        public string HOLD_LOGINNAME__EXACT { get; set; }
        public string RETURN_LOGINNAME__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }

        public HisHoreHohaViewFilter()
            : base()
        {
        }
    }
}
