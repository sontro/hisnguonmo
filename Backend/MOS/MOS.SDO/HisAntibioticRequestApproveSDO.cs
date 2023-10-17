using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAntibioticRequestApproveSDO
    {
        public long AntibioticRequestId { get; set; }
        public bool IsApproved { get; set; }
        public string OtherOpinion { get; set; }
    }
}
