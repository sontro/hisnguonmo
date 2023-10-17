using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisTreatmentView1ImportFilter : FilterBase
    {
        public List<TreatmentImportFilter> TreatmentImportFilters { get; set; }

        public class TreatmentImportFilter
        {
            public string TDL_HEIN_CARD_NUMBER { get; set; }
            public string TDL_PATIENT_CODE { get; set; }
            public string TDL_PATIENT_NAME { get; set; }
            public string TREATMENT_CODE { get; set; }
            public long? IN_TIME { get; set; }
            public long? OUT_TIME { get; set; }
            public List<string> ICD_CODEs { get; set; }
        }
    }
}
