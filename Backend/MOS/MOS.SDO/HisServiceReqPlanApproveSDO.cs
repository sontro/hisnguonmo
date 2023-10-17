using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqPlanApproveSDO
    {
        public long ServiceReqId { get; set; }
        public long WorkingRoomId { get; set; }
        public string Loginname { get; set; }
        public string Username { get; set; }
        public long Time { get; set; }
    }
}