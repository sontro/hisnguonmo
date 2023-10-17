using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class CardBalanceFilter
    {
        public long PATIENT_ID { get; set; }
        public string LAST_DIGITS_OF_BANK_CARD_CODE { get; set; }
    }
}
