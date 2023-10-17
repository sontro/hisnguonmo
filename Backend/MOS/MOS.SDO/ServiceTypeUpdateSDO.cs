using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ServiceTypeUpdateSDO
    {
        public long ServiceTypeId { get; set; }
        public long? NumOrder { get; set; }
        public bool? IsAutoSplitReq { get; set; }
        public bool IsNotDisplayAssign { get; set; }
        public bool IsSplitReqBySampleType { get; set; }
        public bool IsRequiredSampleType { get; set; }
    }
}
