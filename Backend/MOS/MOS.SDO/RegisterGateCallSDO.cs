using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class RegisterGateCallSDO
    {
        public long RegisterGateId { get; set; }
        public string CallPlace { get; set; }
        public long? CallStep { get; set; }
    }
}
