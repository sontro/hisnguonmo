using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisRegisterGateSDO
    {
        public long Id { get; set; }
        public string RegisterGateCode { get; set; }
        public string RegisterGateName { get; set; }
        public short? IsResetAfterNoon { get; set; }
        public long? CurrentNumOrder { get; set; }
        public long? UpdateNumOrder { get; set; }
    }
}
