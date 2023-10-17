using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMedicineTypeView1SDO : V_HIS_MEDICINE_TYPE_1
    {
        public long? MEDICINE_ID { get; set; }
        public decimal? EXP_PRICE { get; set; }
        public decimal? EXP_VAT_RATIO { get; set; }
        public decimal? PRICE { get; set; }
        public long? IMP_TIME { get; set; }
        public decimal? MEDICINE_IMP_PRICE { get; set; }
        public decimal? MEDICINE_IMP_VAT_RATIO { get; set; }
        public short? IS_SALE_EQUAL_IMP_PRICE { get; set; }
    }
}
