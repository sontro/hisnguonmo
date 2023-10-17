using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAccountBookGeneralInfoSDO
    {
        public long AccountBookId { get; set; }
        public decimal? TotalBillAmount { get; set; }
        public string CashierLoginname { get; set; }
        public long? TransactionDate { get; set; }
    }
}
