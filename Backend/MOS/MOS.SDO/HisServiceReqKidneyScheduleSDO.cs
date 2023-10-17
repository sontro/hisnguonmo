using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqKidneyScheduleSDO
    {
        public long TreatmentId { get; set; }
        public long ExecuteTime { get; set; }
        public long KidneyShift { get; set; }
        public long MachineId { get; set; }
        public long RoomId { get; set; }
        public long ExpMestTemplateId { get; set; }
        public long ServiceId { get; set; }
        public long PatientTypeId { get; set; }
        public long WorkingRoomId { get; set; }
        public string Note { get; set; }
        public string RequestLoginname { get; set; }
        public string RequestUsername { get; set; }
    }
}