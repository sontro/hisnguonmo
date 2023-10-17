using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisUpdateServiceExecuteTDO
    {
        public string ServiceReqCode { get; set; }
        public List<string> ServiceCodes { get; set; }
        public bool IsNoExecute { get; set; }
    }
}
