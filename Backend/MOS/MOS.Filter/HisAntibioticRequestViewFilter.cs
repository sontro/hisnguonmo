
namespace MOS.Filter
{
    public class HisAntibioticRequestViewFilter : FilterBase
    {
        public string ANTIBIOTIC_REQUEST_CODE__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string REQUEST_LOGINNAME__EXACT { get; set; }
        public string APPROVAL_LOGINNAME__NULL_OR_EXACT { get; set; }

        public long? REQUEST_DEPARTMENT_ID { get; set; }
        public long? ANTIBIOTIC_REQUEST_STT { get; set; }

        public long? REQUEST_TIME__FROM { get; set; }
        public long? REQUEST_TIME__TO { get; set; }

        public long? APPROVE_TIME__FROM { get; set; }
        public long? APPROVE_TIME__TO { get; set; }

        public HisAntibioticRequestViewFilter()
            : base()
        {
        }
    }
}
