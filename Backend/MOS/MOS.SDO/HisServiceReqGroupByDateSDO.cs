using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqGroupByDateSDO
    {
        public long? InstructionDate { get; set; }
        public long TreatmentId { get; set; }
        public int Total { get; set; }
    }
}
