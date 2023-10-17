
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineMedicineViewFilter : FilterBase
    {
        public long? MEDICINE_ID { get; set; }
        public long? PREPARATION_MEDICINE_ID { get; set; }

        public List<long> MEDICINE_IDs { get; set; }
        public List<long> PREPARATION_MEDICINE_IDs { get; set; }

        public long? PREPARATION_MEDICINE_TYPE_ID { get; set; }
        public long? SUPPLIER_ID { get; set; }

        public List<long> PREPARATION_MEDICINE_TYPE_IDs { get; set; }
        public List<long> SUPPLIER_IDs { get; set; }

        public HisMedicineMedicineViewFilter()
            : base()
        {
        }
    }
}
