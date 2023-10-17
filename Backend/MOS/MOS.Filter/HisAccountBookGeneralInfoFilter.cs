using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisAccountBookGeneralInfoFilter
    {
        public HisAccountBookGeneralInfoFilter()
        {

        }

        public List<long> ACCOUNT_BOOK_IDs { get; set; }
        public string CASHIER_LOGINNAME { get; set; }
        public long? TRANSACTON_DATE { get; set; }
    }
}
