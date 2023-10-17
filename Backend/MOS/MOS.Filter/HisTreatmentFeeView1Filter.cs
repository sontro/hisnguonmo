
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentFeeView1Filter : FilterBase
    {
        public bool? IS_PAUSE { get; set; }
        public long? PATIENT_ID { get; set; }
        public short? IS_LOCK_HEIN { get; set; }
        public string VIR_PATIENT_NAME { get; set; }
        public string PATIENT_CODE { get; set; }
        public string TREATMENT_CODE { get; set; }
        public List<long> PATIENT_IDs { get; set; }
        public List<long> TDL_PATIENT_TYPE_IDs { get; set; }

        public HisTreatmentFeeView1Filter()
            : base()
        {
        }
    }
}
