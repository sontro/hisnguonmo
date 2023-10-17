
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTreatmentLView1Filter
    {
        protected static readonly long NEGATIVE_ID = -1;
        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }
        public string KEY_WORD { get; set; }
        public long? ID { get; set; }
        public short? IS_ACTIVE { get; set; }
        public bool? IS_PAUSE { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public long? LAST_DEPARTMENT_ID { get; set; }
        public long? TDL_TREATMENT_TYPE_ID { get; set; }


        public decimal? TOTAL_HEIN_PRICE_FROM { get; set; }
        public decimal? TOTAL_HEIN_PRICE_TO { get; set; }

        public long? IN_DATE_FROM { get; set; }
        public long? IN_DATE_TO { get; set; }

        public HisTreatmentLView1Filter()
            : base()
        {
        }
    }
}
