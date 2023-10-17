using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisTranPatiTempFilter : FilterBase
    {
        public bool? IS_PUBLIC { get; set; }

        public string MEDI_ORG_CODE__EXACT { get; set; }
        public string TRAN_PATI_TEMP_CODE__EXACT { get; set; }

        public long? TRAN_PATI_REASON_ID { get; set; }
        public long? TRAN_PATI_FORM_ID { get; set; }
        public long? TRAN_PATI_TECH_ID { get; set; }

        public List<long> TRAN_PATI_REASON_IDs { get; set; }
        public List<long> TRAN_PATI_FORM_IDs { get; set; }
        public List<long> TRAN_PATI_TECH_IDs { get; set; }

        public HisTranPatiTempFilter()
            : base()
        {
        }
    }
}
