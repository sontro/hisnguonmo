using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisCoTreatmentReceiveSDO
    {
        public long Id { get; set; }
        public long StartTime { get; set; }
        public long BedRoomId { get; set; }
        public long? BedId { get; set; }
        public long? BedServiceId { get; set; }
        public long RequestRoomId { get; set; }
    }
}
