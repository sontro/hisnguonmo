
using System.Collections.Generic;
namespace MOS.Filter
{
    /// <summary>
    /// Filter dac biet, ko extend tu filterBase
    /// </summary>
    public class HisMatyStockWithImpStockViewFilter
    {
        public long? MEDI_STOCK_ID { get; set; }
        public long? IMP_MEDI_STOCK_ID { get; set; }
        public bool? IS_LEAF { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public bool? IS_GOODS_RESTRICT { get; set; }
        public bool? IS_AVAILABLE { get; set; }
        public string ORDER_DIRECTION { get; set; }
        public string ORDER_FIELD { get; set; }
        public long? EXP_DATE_FROM { get; set; }
        public long? EXP_DATE_TO { get; set; }

        public HisMatyStockWithImpStockViewFilter()
        {
        }
    }
}
