using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestGroupByTreatmentFilter
    {
        public string TREATMENT_CODE__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }
        public string KEYWORD { get; set; }
        public long? OUT_TIME_FROM { get; set; }
        public long? OUT_TIME_TO { get; set; }
        public bool? TREATMENT_IS_PAUSE { get; set; }
        public bool? TREATMENT_IS_ACTIVE { get; set; }
        public long? EXP_MEST_STT_ID { get; set; }
        public long? EXP_MEST_TYPE_ID { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public List<long> EXP_MEST_TYPE_IDs { get; set; }
        public string EXP_MEST_CODE__EXACT { get; set; }
        public bool? IS_NOT_TAKEN { get; set; }

        public HisExpMestGroupByTreatmentFilter()
            : base()
        {
        }
    }
}
