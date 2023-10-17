using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPreviousPrescriptionSDO
    {
        public long? USE_TIME_TO { get; set; }
        public long REQUEST_ROOM_ID { get; set; }
        public string REQUEST_ROOM_NAME { get; set; }
        public string TREATMENT_CODE { get; set; }
        public string SERVICE_REQ_CODE { get; set; }
        public long INTRUCTION_TIME { get; set; }
    }
}
