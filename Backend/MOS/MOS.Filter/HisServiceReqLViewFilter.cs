
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceReqLViewFilter
    {
        protected static readonly long NEGATIVE_ID = -1;

        public List<long> IDs { get; set; }
        public List<long> PARENT_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> SERVICE_REQ_STT_IDs { get; set; }
        public List<long> DHST_IDs { get; set; }
        public List<long> SERVICE_REQ_TYPE_IDs { get; set; }
        public List<long> NOT_IN_SERVICE_REQ_TYPE_IDs { get; set; }
        public List<long> NOT_IN_IDs { get; set; }
        public List<long> REQUEST_ROOM_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> TDL_TREATMENT_TYPE_IDs { get; set; }
        public List<long> TDL_PATIENT_TYPE_IDs { get; set; }
        public List<long> SERVICE_IDs { get; set; }

        public string SERVICE_REQ_CODE__ENDS_WITH { get; set; }
        public string SERVICE_REQ_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME__PATIENT_CODE { get; set; }
        public string KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME { get; set; }

        public long? ID { get; set; }
        public long? REQUEST_ROOM_ID__OR__EXECUTE_ROOM_ID { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? PARENT_ID { get; set; }
        public long? SERVICE_REQ_TYPE_ID { get; set; }
        public long? SERVICE_REQ_STT_ID { get; set; }
        public long? INTRUCTION_TIME_FROM { get; set; }
        public long? INTRUCTION_TIME_TO { get; set; }
        public long? REQUEST_DEPARTMENT_ID { get; set; }
        public long? DHST_ID { get; set; }
        public long? REQUEST_ROOM_ID { get; set; }
        public long? EXECUTE_ROOM_ID { get; set; }
        public long? INTRUCTION_DATE__EQUAL { get; set; }
        public long? VIR_INTRUCTION_MONTH__EQUAL { get; set; }
        public long? TDL_TREATMENT_TYPE_ID { get; set; }
        public long? TDL_PATIENT_TYPE_ID { get; set; }

        public long? INTRUCTION_DATE_FROM { get; set; }
        public long? INTRUCTION_DATE_TO { get; set; }

        public List<decimal> INTRUCTION_MONTHs { get; set; }
        public long? TDL_KSK_CONTRACT_ID { get; set; }
        public bool? IS_ENOUGH_SUBCLINICAL_PRES { get; set; }

        public bool? IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY { get; set; }
        public bool? HAS_EXECUTE { get; set; }
        public bool? IS_NOT_IN_DEBT { get; set; }
        public bool? IS_RESULTED { get; set; }
        public bool? IS_NOT_KSK_REQURIED_APROVAL__OR__IS_KSK_APPROVE { get; set; }
        public bool? HAS_CALL_COUNT { get; set; }
        public bool? IS_RESTRICTED_KSK { get; set; }

        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }

        public string ORDER_FIELD1 { get; set; }
        public string ORDER_DIRECTION1 { get; set; }
        public string ORDER_FIELD2 { get; set; }
        public string ORDER_DIRECTION2 { get; set; }
        public string ORDER_FIELD3 { get; set; }
        public string ORDER_DIRECTION3 { get; set; }
        public string ORDER_FIELD4 { get; set; }
        public string ORDER_DIRECTION4 { get; set; }
        public string BED_CODE__BED_NAME { get; set; }

        public HisServiceReqLViewFilter()
            : base()
        {
        }
    }
}
