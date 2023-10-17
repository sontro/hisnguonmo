using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class KskAccessSDO
    {
        public long KskContractId { get; set; }
        public List<long> EmployeeIds { get; set; }
    }

    public class KskAccessResultSDO
    {
        public HIS_KSK_CONTRACT KskContract { get; set; }
        public List<HIS_KSK_ACCESS> KskAccesses { get; set; }
    }
}
