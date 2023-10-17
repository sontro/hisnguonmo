
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceReqView12Filter : FilterBase
    {
        public string SERVICE_REQ_CODE { get; set; }
        public string SESSION_CODE__EXACT { get; set; }
        public string TDL_TREATMENT_CODE__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public string KEYWORD__SERVICE_REQ_CODE__PATIENT_NAME { get; set; }
        public string KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME { get; set; }

        public long? EXECUTE_ROOM_ID { get; set; }
        public long? REQUEST_ROOM_ID { get; set; }
        public long? INTRUCTION_TIME_FROM { get; set; }
        public long? INTRUCTION_TIME_TO { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? SERVICE_REQ_TYPE_ID { get; set; }
        public long? EXECUTE_DEPARTMENT_ID { get; set; }
        public long? FINISH_TIME_FROM { get; set; }
        public long? FINISH_TIME_TO { get; set; }
        public long? START_TIME_FROM { get; set; }
        public long? START_TIME_TO { get; set; }
        public long? REQUEST_DEPARTMENT_ID { get; set; }
        public long? TRACKING_ID { get; set; }
        public long? INTRUCTION_DATE_FROM { get; set; }
        public long? INTRUCTION_DATE_TO { get; set; }
        public long? RATION_TIME_ID { get; set; }
        public long? RATION_SUM_ID { get; set; }
        public long? TREATMENT_TYPE_ID { get; set; }

        public List<long> SERVICE_REQ_STT_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> SERVICE_REQ_TYPE_IDs { get; set; }
        public List<long> EXECUTE_DEPARTMENT_IDs { get; set; }
        public List<long> REQUEST_DEPARTMENT_IDs { get; set; }
        public List<long> TRACKING_IDs { get; set; }
        public List<long> RATION_TIME_IDs { get; set; }
        public List<long> RATION_SUM_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> NOT_IN_SERVICE_REQ_TYPE_IDs { get; set; }
        public List<long> TREATMENT_TYPE_IDs { get; set; }

        public bool? HAS_TRACKING_ID { get; set; }
        public bool? HAS_RATION_SUM_ID { get; set; }
        public bool? IS_REQUEST_EQUAL_EXECUTE { get; set; }
        public bool? HAS_EXECUTE { get; set; }
        public bool? IS_SHOW { get; set; }

        public string ORDER_FIELD1 { get; set; }
        public string ORDER_DIRECTION1 { get; set; }
        public string ORDER_FIELD2 { get; set; }
        public string ORDER_DIRECTION2 { get; set; }
        public string ORDER_FIELD3 { get; set; }
        public string ORDER_DIRECTION3 { get; set; }
        public string ORDER_FIELD4 { get; set; }
        public string ORDER_DIRECTION4 { get; set; }

        public HisServiceReqView12Filter()
            : base()
        {
        }
    }
}
