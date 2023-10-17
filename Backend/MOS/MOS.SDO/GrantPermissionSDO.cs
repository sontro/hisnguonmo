using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public enum Grantable
    {
        HIS_BID
    }

    public class GrantPermissionSDO
    {
        public Grantable Table { get; set; }
        public long DataId { get; set; }
        public List<string> LoginNames { get; set; }
    }
}
