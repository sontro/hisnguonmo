using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    class RationRequest
    {
        public long PatientTypeId { get; set; }
        public long RoomId { get; set; }
        public decimal Amount { get; set; }
        public long ServiceId { get; set; }
        public long RationTimeId { get; set; }
        public long IntructionTime { get; set; }
        public string InstructionNote { get; set; }
    }
}
