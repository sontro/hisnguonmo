using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisTreatmentMedicineForEmrFilter
    {
        public string TREATMENT_CODE__EXACT { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? INSTRUCTION_DATE { get; set; }
        public long? INSTRUCTION_DATE_FROM { get; set; }
        public long? INSTRUCTION_DATE_TO { get; set; }

        public bool? IS_OUT_STOCK { get; set; }
        public bool? IS_EXPORT { get; set; }
    }
}
