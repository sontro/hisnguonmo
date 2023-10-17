using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ObservedTimeSDO
    {
        public long TreatmentBedRoomId { get; set; }
        public bool IsUnobserved { get; set; }  // Bo theo doi
        public long ObservedTimeFrom { get; set; }
        public long ObservedTimeTo { get; set; }
    }
}
