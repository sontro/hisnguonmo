using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExecuteRoomAppointedSDO
    {
        public string ExecuteRoomCode { get; set; }
        public string ExecuteRoomName { get; set; }
        public long ExecuteRoomId { get; set; }
        public long? MaxAmount { get; set; }
        public long? CurrentAmount { get; set; }
    }
}
