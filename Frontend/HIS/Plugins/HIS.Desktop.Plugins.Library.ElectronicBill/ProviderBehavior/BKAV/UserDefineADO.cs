using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProviderBehavior.BKAV
{
    class UserDefineADO
    {
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string END_CODE { get; set; }
        public string EXTRA_END_CODE { get; set; }
        public string MAIN_CAUSE { get; set; }
        public string OUT_CODE { get; set; }
        public string STORE_CODE { get; set; }
        public decimal? TOTAL_BILL_AMOUNT { get; set; }
        public decimal? TOTAL_BILL_EXEMPTION { get; set; }
        public decimal? TOTAL_BILL_FUND { get; set; }
        public decimal? TOTAL_BILL_OTHER_AMOUNT { get; set; }
        public decimal? TOTAL_BILL_TRANSFER_AMOUNT { get; set; }
        public decimal? TOTAL_DEBT_AMOUNT { get; set; }
        public decimal? TOTAL_DEPOSIT_AMOUNT { get; set; }
        public decimal? TOTAL_DISCOUNT { get; set; }
        public decimal? TOTAL_HEIN_PRICE { get; set; }
        public decimal? TOTAL_PATIENT_PRICE { get; set; }
        public decimal? TOTAL_PRICE { get; set; }
        public decimal? TOTAL_PRICE_EXPEND { get; set; }
        public decimal? TOTAL_REPAY_AMOUNT { get; set; }
        public string TREATMENT_CODE { get; set; }
        public decimal? TREATMENT_DAY_COUNT { get; set; }
        public string Khoa { get; set; }
    }
}
