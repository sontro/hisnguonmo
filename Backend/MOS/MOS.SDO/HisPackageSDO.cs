using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPackageDetailSDO
    {
        public long ServiceId { get; set; }
        public decimal Amount { get; set; }
    }

    public class HisPackageSDO
    {
        public long PackageId { get; set; }
        public List<HisPackageDetailSDO> Details { get; set; }
    }
}
