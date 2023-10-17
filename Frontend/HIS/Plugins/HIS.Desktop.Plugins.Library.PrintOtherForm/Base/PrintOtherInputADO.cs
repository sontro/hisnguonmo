using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintOtherForm.Base
{
    public class PrintOtherInputADO
    {
        public long TreatmentId { get; set; }
        public long ServiceReqId { get; set; }
        public long PatientId { get; set; }
        public long? DepartmentId { get; set; }
        public long? TreatmentBedRoomId { get; set; }
        public long? RoomId { get; set; }
        public long? RoomTypeId { get; set; }
        public long? DhstId { get; set; }
        public long? SereServId { get; set; }
        public long? EkipId { get; set; }
    }
}
