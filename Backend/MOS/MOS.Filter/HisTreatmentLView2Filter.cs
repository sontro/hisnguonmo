
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentLView2Filter
    {
        protected static readonly long NEGATIVE_ID = -1;
        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }
        public string KEY_WORD { get; set; }
        public long? ID { get; set; }
        public bool? IS_PAUSE { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public long? PATIENT_ID { get; set; }

        public HisTreatmentLView2Filter()
            : base()
        {
        }
    }
}
