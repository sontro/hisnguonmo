using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillOther.ADO
{
    public class HisBillGoodADO : HIS_BILL_GOODS
    {
        public decimal TOTAL_PRICE { get; set; }
        public decimal TOTAL_PRICE_WITH_DISCOUNT { get; set; }

        public string ErrorText { get; set; }
        public bool Error { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorType { get; set; }
        public string ErrorColumnName { get; set; }
    }
}
