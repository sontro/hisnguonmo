using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqFinishSDO
    {
        public long ServiceReqId { get; set; }
        public string ExecuteLoginname { get; set; }
        public string ExecuteUsername { get; set; }
        public long? FinishTime { get; set; }
    }
}
