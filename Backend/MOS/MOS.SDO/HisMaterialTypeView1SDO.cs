using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMaterialTypeView1SDO : V_HIS_MATERIAL_TYPE_1
    {
        public long? MATERIAL_ID { get; set; }
        public decimal? EXP_PRICE { get; set; }
        public decimal? EXP_VAT_RATIO { get; set; }
        public decimal? PRICE { get; set; }
        public long? IMP_TIME { get; set; }
        public decimal? MATERIAL_IMP_PRICE { get; set; }
        public decimal? MATERIAL_IMP_VAT_RATIO { get; set; }
        public short? IS_SALE_EQUAL_IMP_PRICE { get; set; }
    }
}
