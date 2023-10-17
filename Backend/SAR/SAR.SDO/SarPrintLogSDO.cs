using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.SDO
{
    public class SarPrintLogSDO
    {
        public string PrintTypeCode { get; set; }
        public string PrintTypeName { get; set; }
        public string LogginName { get; set; }
        public string Ip { get; set; }
        public string DataContent { get; set; }
        public string UniqueCode { get; set; }
        public long? PrintTime { get; set; }
        public long NumOrder { get; set; }
        public string PrintReason { get; set; }
    }
}
