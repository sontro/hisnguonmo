using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisSereServView8Filter : FilterBase
    {
        public List<long> PTTT_PRIORITY_IDs { get; set; }
        public List<long> SERVICE_TYPE_IDs { get; set; }
        public List<long> REQ_SURG_TREATMENT_TYPE_IDs { get; set; }
        public List<long> TDL_EXECUTE_ROOM_IDs { get; set; }
        public List<long> SERVICE_REQ_STT_IDs { get; set; }

        public long? PTTT_PRIORITY_ID { get; set; }
        public long? SERVICE_TYPE_ID { get; set; }
        public bool? HAS_EXECUTE { get; set; }
        public bool? IS_EXPEND { get; set; }

        public long? END_TIME_FROM { get; set; }
        public long? END_TIME_TO { get; set; }
        public long? BEGIN_TIME_FROM { get; set; }
        public long? BEGIN_TIME_TO { get; set; }
        public long? TDL_HEIN_SERVICE_TYPE_ID { get; set; }
        public long? REQ_SURG_TREATMENT_TYPE_ID { get; set; }
        public long? TDL_EXECUTE_ROOM_ID { get; set; }

        public bool? HAS_SERVICE_PTTT_GROUP_ID { get; set; }
        public bool? IS_FEE { get; set; }
        public bool? IS_GATHER_DATA { get; set; }
        public bool? HAS_EKIP { get; set; }
        public string SERVICE_REQ_CODE__EXACT { get; set; }

        public HisSereServView8Filter()
            : base()
        {

        }
    }
}
