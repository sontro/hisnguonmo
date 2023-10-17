using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestMobaBloodSDO
    {
        public List<long> BloodIds { get; set; }

        public long ExpMestId { get; set; }
        public long RequestRoomId { get; set; }
        public string Description { get; set; }
        public long? TrackingId { get; set; }
    }
}
