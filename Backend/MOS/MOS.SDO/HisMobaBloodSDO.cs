using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMobaBloodSDO
    {
        public long BloodId { get; set; }

        public HisMobaBloodSDO() { }

        public HisMobaBloodSDO(long bloodId)
        {
            this.BloodId = bloodId;
        }
    }
}
