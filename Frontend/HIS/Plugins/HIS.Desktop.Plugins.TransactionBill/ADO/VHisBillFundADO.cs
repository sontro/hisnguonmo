using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBill.ADO
{
    public class VHisBillFundADO : V_HIS_BILL_FUND
    {
        public bool IsNotEdit { get; set; }
        public decimal? FUND_BUDGET { get; set; }
    }
}
