using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMachineCounterSDO : HIS_MACHINE
    {
        public long? TOTAL_PROCESSED_SERVICE { get; set; }
        public long? TOTAL_PROCESSED_SERVICE_TEIN { get; set; }
    }
}
