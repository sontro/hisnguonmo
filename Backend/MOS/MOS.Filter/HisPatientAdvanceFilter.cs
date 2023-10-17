
namespace MOS.Filter
{
    public class HeinCardNumberOrCccdNumber
    {
        public string HEIN_CARD_NUMBER__EXACT { get; set; }
        public string CCCD_NUMBER__EXACT { get; set; }
    }
    public class HisPatientAdvanceFilter
    {
        public long? GENDER_ID { get; set; }
        public long? DOB { get; set; }
        public string VIR_PATIENT_NAME__EXACT { get; set; }

        public string PATIENT_PROGRAM_CODE__EXACT { get; set; }
        public string APPOINTMENT_CODE__EXACT { get; set; }
        public string CARD_CODE__EXACT { get; set; }
        public string HEIN_CARD_NUMBER__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string CMND_NUMBER__EXACT { get; set; }
        public string CCCD_NUMBER__EXACT { get; set; }
        public string HRM_EMPLOYEE_CODE__EXACT { get; set; }
        public string PHONE__EXACT { get; set; }
        public string SERVICE_CODE__EXACT { get; set; }
        public HeinCardNumberOrCccdNumber HEIN_CARD_NUMBER_OR_CCCD_NUMBER { get; set; }

        public HisPatientAdvanceFilter()
            : base()
        {

        }
    }
}
