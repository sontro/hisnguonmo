using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisServiceReqResultForEmrFilter
    {
        public long? TREATMENT_ID { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public long? INSTRUCTION_DATE__FROM { get; set; }
        public long? INSTRUCTION_DATE__TO { get; set; }
    }
}
