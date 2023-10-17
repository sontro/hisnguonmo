using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBill.ADO
{
    public class InformationBuyerADO
    {
        public string FullName { get; set; }
        public string TaxCode { get; set; }
        public string AccountNumber { get; set; }
        public long? UnitID { get; set; }
        public string Address { get; set; }
        public string UnitText { get; set; }
        public string checkBox { get; set; }
    }
}
