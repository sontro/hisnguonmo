using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestBcsMoreInfoSDO
    {
        public long ExpMestId { get; set; }
        public List<string> TreatmentCodes { get; set; }
        public List<string> ExpMestCodes { get; set; }
        public int PrescriptionCount { get; set; }
    }
}
