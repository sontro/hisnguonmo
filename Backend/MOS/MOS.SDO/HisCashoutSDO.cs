using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisCashoutSDO
    {
        public long Id { get; set; }
        public long CashoutTime { get; set; }
        public decimal Amount { get; set; }
        public List<long> TransactionIds { get; set; }
    }
}
