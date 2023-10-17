
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineTypeTutFilter : FilterBase
    {
        public long? MEDICINE_TYPE_ID { get; set; }
        public string LOGINNAME__EXACT { get; set; }

        public long? HTU_ID { get; set; }

        public List<long> HTU_IDs { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }

        public HisMedicineTypeTutFilter()
            : base()
        {
        }
    }
}
