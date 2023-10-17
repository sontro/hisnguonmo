
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceReqFilter : FilterBase
    {
        public List<long> PARENT_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> TRACKING_IDs { get; set; }
        public List<long> SERVICE_REQ_STT_IDs { get; set; }
        public List<long> PAAN_POSITION_IDs { get; set; }
        public List<long> PAAN_LIQUID_IDs { get; set; }
        public List<long> REHA_SUM_IDs { get; set; }
        public List<long> DHST_IDs { get; set; }
        public List<long> SERVICE_REQ_TYPE_IDs { get; set; }
        public List<long> TDL_TREATMENT_TYPE_IDs { get; set; }
        public List<long> NOT_IN_SERVICE_REQ_TYPE_IDs { get; set; }
        public List<long> NOT_IN_IDs { get; set; }
        public List<long> REQUEST_ROOM_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> PREVIOUS_SERVICE_REQ_IDs { get; set; }
        public List<long> RATION_TIME_IDs { get; set; }
        public List<long> RATION_SUM_IDs { get; set; }
        public List<long> PTTT_CALENDAR_IDs { get; set; }
        public List<long> PTTT_APPROVAL_STT_IDs { get; set; }
        public List<long> MACHINE_IDs { get; set; }
        public List<long> KIDNEY_SHIFTs { get; set; }
        public List<long> PATIENT_CLASSIFY_IDs { get; set; }
        public List<long> REQUEST_DEPARTMENT_IDs { get; set; }
        public List<long> CARER_CARD_BORROW_IDs { get; set; }
        public List<long> ATTACHED_IDs { get; set; }

        public string SERVICE_REQ_CODE__ENDS_WITH { get; set; }
        public string BARCODE__EXACT { get; set; }
        public string SERVICE_REQ_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string SESSION_CODE__EXACT { get; set; }
        public string KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME__PATIENT_CODE { get; set; }
        public string KEYWORD__SERVICE_REQ_CODE__TREATMENT_CODE__PATIENT_NAME { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public string BLOCK__EXACT { get; set; }
        public string ASSIGN_TURN_CODE__EXACT { get; set; }
        public string REQUEST_LOGINNAME__EXACT { get; set; }

        public long? REQUEST_DEPARTMENT_ID__OR__EXECUTE_DEPARTMENT_ID { get; set; }
        public long? REQUEST_ROOM_ID__OR__EXECUTE_ROOM_ID { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? PARENT_ID { get; set; }
        public long? TDL_PATIENT_ID { get; set; }
        public long? SERVICE_REQ_TYPE_ID { get; set; }
        public long? SERVICE_REQ_STT_ID { get; set; }
        public long? EXECUTE_GROUP_ID { get; set; }
        public long? INTRUCTION_TIME_FROM { get; set; }
        public long? INTRUCTION_TIME_TO { get; set; }
        public long? FINISH_TIME_FROM { get; set; }
        public long? FINISH_TIME_TO { get; set; }
        public long? EXECUTE_DEPARTMENT_ID { get; set; }
        public long? REQUEST_DEPARTMENT_ID { get; set; }
        public long? TRACKING_ID { get; set; }
        public bool? HAS_EXECUTE { get; set; }
        public long? PAAN_POSITION_ID { get; set; }
        public long? PAAN_LIQUID_ID { get; set; }
        public long? REHA_SUM_ID { get; set; }
        public long? DHST_ID { get; set; }
        public long? REQUEST_ROOM_ID { get; set; }
        public long? INTRUCTION_DATE_FROM { get; set; }
        public long? INTRUCTION_DATE_TO { get; set; }
        public long? EXECUTE_ROOM_ID { get; set; }
        public long? SAMPLE_ROOM_ID { get; set; }
        public long? PREVIOUS_SERVICE_REQ_ID { get; set; }
        public long? RATION_TIME_ID { get; set; }
        public long? RATION_SUM_ID { get; set; }
        public long? PTTT_CALENDAR_ID { get; set; }
        public long? PTTT_APPROVAL_STT_ID { get; set; }
        public long? TDL_TREATMENT_TYPE_ID { get; set; }
        public long? KIDNEY_SHIFT { get; set; }
        public long? MACHINE_ID { get; set; }
        public long? ID__NOT_EQUAL { get; set; }
        public long? PATIENT_CLASSIFY_ID { get; set; }
        public long? CARER_CARD_BORROW_ID { get; set; }
        public long? USE_TIME { get; set; }
        public long? USE_TIME_FROM { get; set; }
        public long? USE_TIME_TO { get; set; }
        public long? USED_FOR_TRACKING_ID { get; set; }
        public long? TRACKING_ID__OR__USED_FOR_TRACKING_ID { get; set; }
        public long? BED_LOG_ID { get; set; }

        public long? INTRUCTION_DATE__EQUAL { get; set; }
        public bool? IS_SEND_LIS { get; set; }
        public bool? IS_SEND_PACS { get; set; }
        public bool? IS_MAIN_EXAM { get; set; }
        public bool? IS_NOT_SENT__OR__UPDATED { get; set; }
        public bool? IS_NOT_SURGERY__OR__IS__APPROVED__OR__IS_EMERGENCY { get; set; }
        public bool? HAS_RATION_SUM_ID { get; set; }
        public bool? HAS_PTTT_CALENDAR_ID { get; set; }
        public bool? IS_KIDNEY { get; set; }
        public bool? IS_NO_EXECUTE { get; set; }
        public bool? HAS_ATTACH_ASSIGN_PRINT_TYPE_CODE { get; set; }
        public bool? IS_NOT_KSK_REQURIED_APROVAL__OR__IS_KSK_APPROVE { get; set; }
        public bool? IS_HOME_PRES { get; set; }
        public bool? IS_INFORM_RESULT_BY_SMS { get; set; }
        public bool? IS_NOT_IN_DEBT { get; set; }
        public bool? HAS_RESULTING_ORDER { get; set; }
        public bool? HAS_SAMPLED { get; set; }
        public bool? HAS_TRACKING_ID { get; set; }
        public List<long> SERVICE_IDs { get; set; }
        public long? TDL_KSK_CONTRACT_ID { get; set; }
        public bool? IS_ENOUGH_SUBCLINICAL_PRES { get; set; }
        public bool? IS_FOR_AUTO_CREATE_RATION { get; set; }
        public bool? IS_RESTRICTED_KSK { get; set; }
        public bool? HAS_TDL_KSK_CONTRACT_ID { get; set; }
        public bool? ALLOW_SEND_PACS { get; set; }

        public string ORDER_FIELD1 { get; set; }
        public string ORDER_DIRECTION1 { get; set; }
        public string ORDER_FIELD2 { get; set; }
        public string ORDER_DIRECTION2 { get; set; }
        public string ORDER_FIELD3 { get; set; }
        public string ORDER_DIRECTION3 { get; set; }
        public string ORDER_FIELD4 { get; set; }
        public string ORDER_DIRECTION4 { get; set; }

        public long? USE_TIME_OR_INTRUCTION_TIME_FROM { get; set; }
        public long? USE_TIME_OR_INTRUCTION_TIME_TO { get; set; }
        public List<string> SERVICE_REQ_CODES { get; set; }

        public HisServiceReqFilter()
            : base()
        {
        }
    }
}
