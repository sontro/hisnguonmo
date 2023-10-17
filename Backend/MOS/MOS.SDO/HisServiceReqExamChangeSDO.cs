using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqExamChangeSDO
    {
        public long CurrentServiceReqId { get; set; }
        public long ServiceId { get; set; }
        public long PatientTypeId { get; set; }
        public long InstructionTime { get; set; }
        public bool Priority { get; set; }
        public bool IsNotRequireFee { get; set; }
        public long RequestRoomId { get; set; }
        public long RoomId { get; set; }
        public bool IsCopyOldInfo { get; set; }
        public long? PrimaryPatientTypeId { get; set; }

    }
}
