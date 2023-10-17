using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AnticipateCreate.ADO
{
    class BidADO
    {
        public string BID_NUMBER { get; set; }
        public string BID_NAME { get; set; }
        public string BID_YEAR { get; set; }
        public decimal? AMOUNT { get; set; }
        public long ID { get; set; }
        public long SUPPLIER_ID { get; set; }

    }
}
