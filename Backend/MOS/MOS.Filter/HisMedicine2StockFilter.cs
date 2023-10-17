using System.Collections.Generic;

namespace MOS.Filter
{
    /// <summary>
    /// Filter dac biet, ko extend tu FilterBase, do co nhieu field cua filterBase ko duoc dung den
    /// </summary>
    public class HisMedicine2StockFilter
    {
        public long FIRST_MEDI_STOCK_ID { get; set; }
        public long SECOND_MEDI_STOCK_ID { get; set; }

        public List<long> FIRST_MEDI_STOCK_IDs { get; set; }

        public short? IS_LEAF { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public bool? MEDICINE_TYPE_IS_ACTIVE { get; set; }

        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }

        public HisMedicine2StockFilter()
        {
        }
    }
}
