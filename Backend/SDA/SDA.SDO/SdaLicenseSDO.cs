using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDA.SDO
{
    public class SdaLicenseSDO
    {
        public string License { get; set; }
        public string ClientCode { get; set; }
        public string AppCode { get; set; }
        public long? ExpiredDate { get; set; }
    }
}
