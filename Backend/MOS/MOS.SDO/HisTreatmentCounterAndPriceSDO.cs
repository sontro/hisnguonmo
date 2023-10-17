using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentCounterAndPriceSDO
    {
        public int COUNT_TREATMENT_EXAM { get; set; }
        public int COUNT_TREATMENT_IN { get; set; }
        public int COUNT_TREATMENT_TRAN { get; set; }

        public decimal? TOTAL_PRICE { get; set; }
        public decimal? TOTAL_PATIENT_PRICE { get; set; }
        public decimal? TOTAL_HEIN_PRICE { get; set; }
        public decimal? TOTAL_BILL_AMOUNT { get; set; }
        public decimal? TOTAL_DEPOSIT_AMOUNT { get; set; }
        public decimal? TOTAL_REPAY_AMOUNT { get; set; }
    }
}
