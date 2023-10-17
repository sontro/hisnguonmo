using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceRatiSDO
    {
        public long ServiceId { get; set; }
        public List<long> RationTimeIds { get; set; }
    }
}
