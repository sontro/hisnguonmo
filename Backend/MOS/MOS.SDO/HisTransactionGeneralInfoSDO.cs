using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransactionGeneralInfoSDO
    {
        public string CashierLoginname { get; set; }
        public long? TransactionDate { get; set; }

        public decimal TotalBillNotDirectly { get; set; }
        public decimal TotalBillDirectly { get; set; }
    }
}
