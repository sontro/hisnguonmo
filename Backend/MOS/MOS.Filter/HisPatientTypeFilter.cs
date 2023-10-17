
namespace MOS.Filter
{
    public class HisPatientTypeFilter : FilterBase
    {
        public bool? IS_COPAYMENT { get; set; }
        public bool? IS_CHECK_FINISH_CLS_WHEN_PRES { get; set; }
        public string PATIENT_TYPE_CODE__EXACT { get; set; }

        public HisPatientTypeFilter()
            : base()
        {

        }
    }
}
