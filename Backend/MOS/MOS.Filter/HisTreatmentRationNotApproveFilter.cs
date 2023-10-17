using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisTreatmentRationNotApproveFilter
    {
        public long? TREATMENT_ID { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
    }
}
