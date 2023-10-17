
using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisDhstViewFilter : FilterBase
    {
        public enum RecordTypeBelong
        {
            CARE,
            TRACKING,
            EXAM
        }

        public string EXECUTE_LOGINNAME__EXACT { get; set; }
        public string DEPARTMENT_CODE__EXACT { get; set; }
        public string ROOM_CODE__EXACT { get; set; }

        public long? TREATMENT_ID { get; set; }
        public long? TRACKING_ID { get; set; }
        public long? CARE_ID { get; set; }
        public long? DHST_SUM_ID { get; set; }
        public long? EXECUTE_ROOM_ID { get; set; }
        public long? EXECUTE_DEPARTMENT_ID { get; set; }
        public long? VACCINATION_EXAM_ID { get; set; }
        public List<long> CARE_IDs { get; set; }
        public List<long> VACCINATION_EXAM_IDs { get; set; }
        public List<long> TRACKING_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> DHST_SUM_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> EXECUTE_DEPARTMENT_IDs { get; set; }

        public long? EXECUTE_TIME_FROM { get; set; }
        public long? EXECUTE_TIME_TO { get; set; }

        public bool? IS_IN_SERVICE_REQ { get; set; }

        public bool? HAS_CARE_ID { get; set; }
        public bool? HAS_TRACKING_ID { get; set; }
        public bool? HAS_CARE_OR_TRACKING_ID { get; set; }

        public List<RecordTypeBelong> RECORD_TYPE_BELONGs { get; set; }

        public HisDhstViewFilter()
            : base()
        {
        }
    }
}
