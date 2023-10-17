using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class TakeBeanBySerialSDO
    {
        public string SerialNumber { get; set; } //So serial
        public long MediStockId { get; set; }
        public long PatientTypeId { get; set; }
        public string ClientSessionKey { get; set; }
    }
}
