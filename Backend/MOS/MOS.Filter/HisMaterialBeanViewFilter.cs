using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisMaterialBeanViewFilter : FilterBase
    {
        public long? MEDI_STOCK_ID {get;set;}
        public long? MATERIAL_TYPE_ID { get; set; }
        public long? MATERIAL_ID { get; set; }
        public bool? MATERIAL_TYPE_IS_ACTIVE { get; set; }
        public bool? MATERIAL_IS_ACTIVE { get; set; }
        public long? EXP_MEST_MATERIAL_ID { get; set; }

        public List<long> EXP_MEST_MATERIAL_IDs { get; set; }
        public List<long> MEDI_STOCK_IDs { get; set; }
        public List<long> MATERIAL_TYPE_IDs { get; set; }
        public List<long> MATERIAL_IDs { get; set; }
        public List<long> SOURCE_IDs { get; set; }
        public bool? IS_GOODS_RESTRICT { get; set; }

        public long? EXPIRED_DATE__NULL_OR_GREATER_THAN_OR_EQUAL { get; set; }

        /// <summary>
        /// Bean co dang o trong kho hay khong (medi_stock_id null or != null)
        /// </summary>
        public enum InStockEnum
        {
            YES,
            NO,
        }
        public InStockEnum? IN_STOCK { get; set; }
        public bool? IS_REUSABLE { get; set; }
        
        public HisMaterialBeanViewFilter()
            : base()
        {
        }
    }
}
