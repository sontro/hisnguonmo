
using System.Collections.Generic;
namespace MOS.Filter
{
    /// <summary>
    /// Filter dac biet, ko extend tu filterBase
    /// </summary>
    public class HisMedicineTypeHospitalViewFilter
    {
        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> MEDI_STOCK_IDs { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public short? IS_ACTIVE { get; set; }
        public long? ID { get; set; }
        public bool? IS_BUSINESS { get; set; }

        public HisMedicineTypeHospitalViewFilter()
        {
        }
    }
}
