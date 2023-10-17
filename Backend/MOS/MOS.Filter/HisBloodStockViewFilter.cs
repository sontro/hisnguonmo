using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    /// <summary>
    /// Filter dac biet, ko extend tu filterBase
    /// </summary>
    public class HisBloodStockViewFilter
    {
        public List<long> MEDI_STOCK_IDs { get; set; }
        //public bool? IS_AVAILABLE { get; set; }
        public short? IS_ACTIVE { get; set; }
        public long? BLOOD_TYPE_ID { get; set; }
        public List<long> IDs { get; set; }
        public long? EXPIRED_DATE_FROM { get; set; }
        public long? EXPIRED_DATE_TO { get; set; }

        public HisBloodStockViewFilter()
        {
        }
    }
}
