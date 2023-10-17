using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisRegisterGateCurrentNumOrderFilter
    {
        public short? IS_ACTIVE { get; set; }
        public long? ID { get; set; }
        public List<long> IDs { get; set; }
        public long? REGISTER_DATE { get; set; }
    }
}
