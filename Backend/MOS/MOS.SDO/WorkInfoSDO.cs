using System.Collections.Generic;

namespace MOS.SDO
{
    public class RoomSDO
    {
        public long RoomId { get; set; }
        public long? DeskId { get; set; }
    }

    public class WorkInfoSDO
    {
        public List<long> RoomIds { get;set; }
        public List<RoomSDO> Rooms { get; set; }
        public long? WorkingShiftId { get;set; }
        public string NurseLoginName { get; set; }
        public string NurseUserName { get; set; }

        public WorkInfoSDO()
        {
        }
    }
}
