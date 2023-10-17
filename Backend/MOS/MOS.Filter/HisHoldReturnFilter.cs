
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisHoldReturnFilter : FilterBase
    {
        
        public string HEIN_CARD_NUMBER__EXACT { get; set; }
        public string RETURN_LOGINNAME__EXACT { get; set; }        
        public string HOLD_LOGINNAME__EXACT { get; set; }

        public long? PATIENT_ID { get; set; }
        public long? HOLD_ROOM_ID { get; set; }
        public long? RESPONSIBLE_ROOM_ID { get; set; }
        public long? RETURN_ROOM_ID { get; set; }
        public long? TREATMENT_ID { get; set; }

        public List<long> PATIENT_IDs { get; set; }
        public List<long> HOLD_ROOM_IDs { get; set; }
        public List<long> RESPONSIBLE_ROOM_IDs { get; set; }
        public List<long> RETURN_ROOM_IDs { get; set; }

        public long? RETURN_TIME_FROM { get; set; }
        public long? RETURN_TIME_TO { get; set; }
        public long? HOLD_TIME_FROM { get; set; }
        public long? HOLD_TIME_TO { get; set; }

        public bool? IS_HANDOVERING { get; set; }
        public bool? HAS_RETURN_TIME { get; set; }

        public List<long> IS_NOT_HANDOVERING_OR_IDs { get; set; }

        public HisHoldReturnFilter()
            : base()
        {
        }
    }
}
