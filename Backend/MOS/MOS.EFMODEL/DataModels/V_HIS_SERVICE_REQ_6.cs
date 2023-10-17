//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MOS.EFMODEL.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HIS_SERVICE_REQ_6
    {
        public long ID { get; set; }
        public Nullable<long> CREATE_TIME { get; set; }
        public Nullable<long> MODIFY_TIME { get; set; }
        public string CREATOR { get; set; }
        public string MODIFIER { get; set; }
        public string APP_CREATOR { get; set; }
        public string APP_MODIFIER { get; set; }
        public Nullable<short> IS_ACTIVE { get; set; }
        public Nullable<short> IS_DELETE { get; set; }
        public string GROUP_CODE { get; set; }
        public string SERVICE_REQ_CODE { get; set; }
        public long SERVICE_REQ_TYPE_ID { get; set; }
        public long SERVICE_REQ_STT_ID { get; set; }
        public long TREATMENT_ID { get; set; }
        public long INTRUCTION_TIME { get; set; }
        public long INTRUCTION_DATE { get; set; }
        public long REQUEST_ROOM_ID { get; set; }
        public long REQUEST_DEPARTMENT_ID { get; set; }
        public string REQUEST_LOGINNAME { get; set; }
        public string REQUEST_USERNAME { get; set; }
        public long EXECUTE_ROOM_ID { get; set; }
        public long EXECUTE_DEPARTMENT_ID { get; set; }
        public string EXECUTE_LOGINNAME { get; set; }
        public string EXECUTE_USERNAME { get; set; }
        public Nullable<long> EXE_SERVICE_MODULE_ID { get; set; }
        public Nullable<long> START_TIME { get; set; }
        public Nullable<long> FINISH_TIME { get; set; }
        public Nullable<long> ICD_ID__DELETE { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_SUB_CODE { get; set; }
        public string ICD_TEXT { get; set; }
        public string ICD_CAUSE_CODE { get; set; }
        public string ICD_CAUSE_NAME { get; set; }
        public Nullable<long> NUM_ORDER { get; set; }
        public Nullable<long> ESTIMATE_TIME_FROM { get; set; }
        public Nullable<long> ESTIMATE_TIME_TO { get; set; }
        public Nullable<short> IS_HOLD_ORDER { get; set; }
        public Nullable<long> PRIORITY { get; set; }
        public Nullable<long> TRACKING_ID { get; set; }
        public Nullable<long> PARENT_ID { get; set; }
        public Nullable<long> PREVIOUS_SERVICE_REQ_ID { get; set; }
        public Nullable<long> TREATMENT_TYPE_ID { get; set; }
        public Nullable<short> IS_WAIT_CHILD { get; set; }
        public Nullable<long> DHST_ID { get; set; }
        public Nullable<long> EXECUTE_GROUP_ID { get; set; }
        public Nullable<long> ASSIGN_REASON_ID { get; set; }
        public Nullable<short> IS_NOT_REQUIRE_FEE { get; set; }
        public Nullable<short> IS_NO_EXECUTE { get; set; }
        public Nullable<long> CALL_COUNT { get; set; }
        public string JSON_PRINT_ID { get; set; }
        public string JSON_FORM_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string SESSION_CODE { get; set; }
        public Nullable<short> IS_EMERGENCY { get; set; }
        public Nullable<short> IS_NOT_SHOW_MATERIAL_TRACKING { get; set; }
        public Nullable<short> IS_NOT_SHOW_MEDICINE_TRACKING { get; set; }
        public string BARCODE { get; set; }
        public Nullable<long> SAMPLE_ROOM_ID { get; set; }
        public Nullable<long> LIS_STT_ID { get; set; }
        public Nullable<short> IS_SENT_EXT { get; set; }
        public Nullable<short> IS_UPDATED_EXT { get; set; }
        public Nullable<short> IS_INFORM_RESULT_BY_SMS { get; set; }
        public Nullable<short> PACS_STT_ID { get; set; }
        public Nullable<long> PAAN_POSITION_ID { get; set; }
        public Nullable<long> PAAN_LIQUID_ID { get; set; }
        public Nullable<long> LIQUID_TIME { get; set; }
        public string ECG_BEFORE { get; set; }
        public string ECG_AFTER { get; set; }
        public string RESPIRATORY_BEFORE { get; set; }
        public string RESPIRATORY_AFTER { get; set; }
        public string SYMPTOM_BEFORE { get; set; }
        public string SYMPTOM_AFTER { get; set; }
        public string ADVISE { get; set; }
        public Nullable<long> REHA_SUM_ID { get; set; }
        public Nullable<short> PRESCRIPTION_TYPE_ID { get; set; }
        public Nullable<long> USE_TIME { get; set; }
        public Nullable<long> USE_TIME_TO { get; set; }
        public Nullable<long> REMEDY_COUNT { get; set; }
        public Nullable<short> IS_MAIN_EXAM { get; set; }
        public string HOSPITALIZATION_REASON { get; set; }
        public string PATHOLOGICAL_PROCESS { get; set; }
        public string PATHOLOGICAL_HISTORY { get; set; }
        public string PATHOLOGICAL_HISTORY_FAMILY { get; set; }
        public string FULL_EXAM { get; set; }
        public string PART_EXAM { get; set; }
        public string PART_EXAM_CIRCULATION { get; set; }
        public string PART_EXAM_RESPIRATORY { get; set; }
        public string PART_EXAM_DIGESTION { get; set; }
        public string PART_EXAM_KIDNEY_UROLOGY { get; set; }
        public string PART_EXAM_NEUROLOGICAL { get; set; }
        public string PART_EXAM_MUSCLE_BONE { get; set; }
        public string PART_EXAM_ENT { get; set; }
        public string PART_EXAM_EAR { get; set; }
        public string PART_EXAM_NOSE { get; set; }
        public string PART_EXAM_THROAT { get; set; }
        public string PART_EXAM_STOMATOLOGY { get; set; }
        public string PART_EXAM_EYE { get; set; }
        public string PART_EXAM_EYE_TENSION_LEFT { get; set; }
        public string PART_EXAM_EYE_TENSION_RIGHT { get; set; }
        public string PART_EXAM_EYESIGHT_LEFT { get; set; }
        public string PART_EXAM_EYESIGHT_RIGHT { get; set; }
        public string PART_EXAM_EYESIGHT_GLASS_LEFT { get; set; }
        public string PART_EXAM_EYESIGHT_GLASS_RIGHT { get; set; }
        public string PART_EXAM_OEND { get; set; }
        public Nullable<long> SICK_DAY { get; set; }
        public string PART_EXAM_MENTAL { get; set; }
        public string PART_EXAM_OBSTETRIC { get; set; }
        public string PART_EXAM_NUTRITION { get; set; }
        public string PART_EXAM_MOTION { get; set; }
        public string NEXT_TREAT_INTR_CODE { get; set; }
        public string NEXT_TREATMENT_INSTRUCTION { get; set; }
        public string SUBCLINICAL { get; set; }
        public string TREATMENT_INSTRUCTION { get; set; }
        public string NOTE { get; set; }
        public Nullable<long> PTTT_CALENDAR_ID { get; set; }
        public Nullable<long> PTTT_APPROVAL_STT_ID { get; set; }
        public Nullable<long> PTTT_APPROVAL_TIME { get; set; }
        public string PTTT_APPROVAL_LOGINNAME { get; set; }
        public string PTTT_APPROVAL_USERNAME { get; set; }
        public Nullable<long> PLAN_TIME_FROM { get; set; }
        public Nullable<long> PLAN_TIME_TO { get; set; }
        public Nullable<long> EKIP_PLAN_ID { get; set; }
        public Nullable<long> RATION_SUM_ID { get; set; }
        public Nullable<long> RATION_TIME_ID { get; set; }
        public Nullable<long> HEALTH_EXAM_RANK_ID { get; set; }
        public string ATTACHMENT_FILE_URL { get; set; }
        public Nullable<short> IS_INTEGRATE_HIS_SENT { get; set; }
        public Nullable<long> MACHINE_ID { get; set; }
        public Nullable<long> KIDNEY_SHIFT { get; set; }
        public Nullable<long> EXP_MEST_TEMPLATE_ID { get; set; }
        public string TDL_TREATMENT_CODE { get; set; }
        public string TDL_HEIN_CARD_NUMBER { get; set; }
        public long TDL_PATIENT_ID { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public string TDL_PATIENT_FIRST_NAME { get; set; }
        public string TDL_PATIENT_LAST_NAME { get; set; }
        public long TDL_PATIENT_DOB { get; set; }
        public Nullable<short> TDL_PATIENT_IS_HAS_NOT_DAY_DOB { get; set; }
        public string TDL_PATIENT_ADDRESS { get; set; }
        public Nullable<long> TDL_PATIENT_GENDER_ID { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public string TDL_PATIENT_CAREER_NAME { get; set; }
        public string TDL_PATIENT_WORK_PLACE { get; set; }
        public string TDL_PATIENT_WORK_PLACE_NAME { get; set; }
        public string TDL_PATIENT_DISTRICT_CODE { get; set; }
        public string TDL_PATIENT_PROVINCE_CODE { get; set; }
        public string TDL_PATIENT_COMMUNE_CODE { get; set; }
        public string TDL_PATIENT_MILITARY_RANK_NAME { get; set; }
        public string TDL_PATIENT_NATIONAL_NAME { get; set; }
        public string TDL_HEIN_MEDI_ORG_CODE { get; set; }
        public string TDL_HEIN_MEDI_ORG_NAME { get; set; }
        public string TDL_PATIENT_AVATAR_URL { get; set; }
        public Nullable<long> TDL_TREATMENT_TYPE_ID { get; set; }
        public Nullable<short> IS_HOME_PRES { get; set; }
        public Nullable<short> IS_NOT_SHOW_OUT_MATE_TRACKING { get; set; }
        public Nullable<short> IS_NOT_SHOW_OUT_MEDI_TRACKING { get; set; }
        public Nullable<short> IS_KIDNEY { get; set; }
        public Nullable<long> KIDNEY_TIMES { get; set; }
        public string VIR_KIDNEY { get; set; }
        public Nullable<short> IS_EXECUTE_KIDNEY_PRES { get; set; }
        public Nullable<long> CALL_SAMPLE_ORDER { get; set; }
        public string BARCODE_TEMP { get; set; }
        public string RESERVED_NUM_ORDER { get; set; }
        public Nullable<short> IS_ANTIBIOTIC_RESISTANCE { get; set; }
        public Nullable<long> TDL_PATIENT_TYPE_ID { get; set; }
        public Nullable<long> CALL_TIME { get; set; }
        public string PROVISIONAL_DIAGNOSIS { get; set; }
        public Nullable<long> CALL_DATE { get; set; }
        public string NUM_ORDER_BASE { get; set; }
        public Nullable<long> PRIORITY_TYPE_ID { get; set; }
        public Nullable<long> PRES_GROUP { get; set; }
        public string TDL_PATIENT_MOBILE { get; set; }
        public string TDL_PATIENT_PHONE { get; set; }
        public Nullable<short> IS_SEND_BARCODE_TO_LIS { get; set; }
        public Nullable<long> EXECUTE_WORKING_SHIFT_ID { get; set; }
        public Nullable<long> EXE_WORKING_SHIFT_ID { get; set; }
        public Nullable<long> REQ_WORKING_SHIFT_ID { get; set; }
        public Nullable<short> HAS_CHILD { get; set; }
        public Nullable<short> IS_STAR_MARK { get; set; }
        public Nullable<long> PATIENT_CASE_ID { get; set; }
        public Nullable<short> IS_AUTO_FINISHED { get; set; }
        public Nullable<short> IS_COLLECTED { get; set; }
        public Nullable<long> TEST_SAMPLE_TYPE_ID { get; set; }
        public Nullable<long> EXE_DESK_ID { get; set; }
        public string TRADITIONAL_ICD_CODE { get; set; }
        public string TRADITIONAL_ICD_NAME { get; set; }
        public string TRADITIONAL_ICD_SUB_CODE { get; set; }
        public string TRADITIONAL_ICD_TEXT { get; set; }
        public Nullable<short> TDL_KSK_IS_REQUIRED_APPROVAL { get; set; }
        public Nullable<short> TDL_IS_KSK_APPROVE { get; set; }
        public string TREAT_EYE_TENSION_LEFT { get; set; }
        public string TREAT_EYE_TENSION_RIGHT { get; set; }
        public string TREAT_EYESIGHT_LEFT { get; set; }
        public string TREAT_EYESIGHT_RIGHT { get; set; }
        public string TREAT_EYESIGHT_GLASS_LEFT { get; set; }
        public string TREAT_EYESIGHT_GLASS_RIGHT { get; set; }
        public Nullable<short> IS_FIRST_OPTOMETRIST { get; set; }
        public string OPTOMETRIST_TIME { get; set; }
        public string FORESIGHT_RIGHT_EYE { get; set; }
        public string FORESIGHT_LEFT_EYE { get; set; }
        public string FORESIGHT_RIGHT_GLASS_HOLE { get; set; }
        public string FORESIGHT_LEFT_GLASS_HOLE { get; set; }
        public string FORESIGHT_RIGHT_USING_GLASS { get; set; }
        public string FORESIGHT_LEFT_USING_GLASS { get; set; }
        public string REFACTOMETRY_RIGHT_EYE { get; set; }
        public string REFACTOMETRY_LEFT_EYE { get; set; }
        public string BEFORE_LIGHT_REFLECTION_RIGHT { get; set; }
        public string BEFORE_LIGHT_REFLECTION_LEFT { get; set; }
        public string AFTER_LIGHT_REFLECTION_RIGHT { get; set; }
        public string AFTER_LIGHT_REFLECTION_LEFT { get; set; }
        public string AJUSTABLE_GLASS_FORESIGHT { get; set; }
        public string AJUSTABLE_GLASS_FORESIGHT_R { get; set; }
        public string AJUSTABLE_GLASS_FORESIGHT_L { get; set; }
        public string NEARSIGHT_GLASS_RIGHT_EYE { get; set; }
        public string NEARSIGHT_GLASS_LEFT_EYE { get; set; }
        public string NEARSIGHT_GLASS_READING_DIST { get; set; }
        public string NEARSIGHT_GLASS_PUPIL_DIST { get; set; }
        public Nullable<long> REOPTOMETRIST_APPOINTMENT { get; set; }
        public string FORESIGHT_USING_GLASS_DEGREE_R { get; set; }
        public string FORESIGHT_USING_GLASS_DEGREE_L { get; set; }
        public string RESULT_APPROVER_LOGINNAME { get; set; }
        public string RESULT_APPROVER_USERNAME { get; set; }
        public Nullable<long> EXAM_END_TYPE { get; set; }
        public string CONSULTANT_LOGINNAME { get; set; }
        public string CONSULTANT_USERNAME { get; set; }
        public Nullable<long> TDL_PATIENT_CLASSIFY_ID { get; set; }
        public string ASSIGNED_EXECUTE_LOGINNAME { get; set; }
        public string ASSIGNED_EXECUTE_USERNAME { get; set; }
        public Nullable<short> IS_NOT_IN_DEBT { get; set; }
        public Nullable<long> RESULT_ROOM_ID { get; set; }
        public Nullable<long> RESULT_DESK_ID { get; set; }
        public Nullable<long> CASHIER_ROOM_ID { get; set; }
        public Nullable<short> IS_RESULT_IN_DIFF_DAY { get; set; }
        public Nullable<decimal> VIR_INTRUCTION_MONTH { get; set; }
        public string BIIN_TEST_RESULT { get; set; }
        public Nullable<long> RESULTING_ORDER { get; set; }
        public Nullable<long> RESULTING_TIME { get; set; }
        public string BLOCK { get; set; }
        public Nullable<short> IS_SAMPLED { get; set; }
        public Nullable<long> SAMPLE_TIME { get; set; }
        public string SAMPLER_LOGINNAME { get; set; }
        public string SAMPLER_USERNAME { get; set; }
        public string TDL_INSTRUCTION_NOTE { get; set; }
        public Nullable<short> IS_RESULTED { get; set; }
        public string ASSIGN_TURN_CODE { get; set; }
        public Nullable<short> IS_NOT_USE_BHYT { get; set; }
        public Nullable<long> BARCODE_LENGTH { get; set; }
        public string PART_EXAM_DERMATOLOGY { get; set; }
        public Nullable<short> IS_ACCEPTING_NO_EXECUTE { get; set; }
        public Nullable<long> EXAM_END_TIME { get; set; }
        public Nullable<long> EXAM_TREATMENT_END_TYPE_ID { get; set; }
        public Nullable<long> EXAM_TREATMENT_RESULT_ID { get; set; }
        public string PART_EXAM_EAR_RIGHT_NORMAL { get; set; }
        public string PART_EXAM_EAR_RIGHT_WHISPER { get; set; }
        public string PART_EXAM_EAR_LEFT_NORMAL { get; set; }
        public string PART_EXAM_EAR_LEFT_WHISPER { get; set; }
        public string PART_EXAM_UPPER_JAW { get; set; }
        public string PART_EXAM_LOWER_JAW { get; set; }
        public Nullable<long> PART_EXAM_HORIZONTAL_SIGHT { get; set; }
        public Nullable<long> PART_EXAM_VERTICAL_SIGHT { get; set; }
        public Nullable<long> PART_EXAM_EYE_BLIND_COLOR { get; set; }
        public string REQUEST_USER_TITLE { get; set; }
        public string EXECUTE_USER_TITLE { get; set; }
        public string SERVICE_REQ_TYPE_CODE { get; set; }
        public string SERVICE_REQ_TYPE_NAME { get; set; }
        public string SERVICE_REQ_STT_CODE { get; set; }
        public string SERVICE_REQ_STT_NAME { get; set; }
    }
}
