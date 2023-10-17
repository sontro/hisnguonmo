using System.Collections.Generic;

namespace MOS.Filter
{
    /// <summary>
    /// Filter dac biet, ko extend tu FilterBase, do co nhieu field cua filterBase ko duoc dung den
    /// </summary>
    public class HisMedicineStockViewFilter
    {
        public List<long> MEDI_STOCK_IDs { get; set; }
        public List<long> IDs { get; set; }

        public long? MEDI_STOCK_ID { get; set; }
        public long? ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }
        public short? IS_LEAF { get; set; }
        public string KEY_WORD { get; set; }
        public bool INCLUDE_EMPTY { get; set; } //co bao gom cac loai thuoc ko co trong kho hay khong
        public bool GROUP_BY_MEDI_STOCK { get; set; }
        public bool? IS_ACTIVE { get; set; }
        public bool? MEDICINE_TYPE_IS_ACTIVE { get; set; }

        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }

        public bool INCLUDE_BASE_AMOUNT { get; set; }//co ca thong tin co so tu truc
        public bool INCLUDE_EXP_PRICE { get; set; }
        public long? EXP_PATIENT_TYPE_ID { get; set; }
        public bool? MEDICINE_IS_ACTIVE { get; set; }

        public HisMedicineStockViewFilter()
        {
        }
    }
}
