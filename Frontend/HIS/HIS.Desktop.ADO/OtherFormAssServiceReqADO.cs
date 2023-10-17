using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class OtherFormAssServiceReqADO
    {
        public long ServiceReqId { get; set; }
        public string PrintTypeCode { get; set; }
        public Dictionary<string, object> DicParam { get; set; }

        public OtherFormAssServiceReqADO() { }
    }
}
