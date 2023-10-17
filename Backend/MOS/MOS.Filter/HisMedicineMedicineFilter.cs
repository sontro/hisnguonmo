
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineMedicineFilter : FilterBase
    {
        public long? MEDICINE_ID { get; set; }
        public long? PREPARATION_MEDICINE_ID { get; set; }

        public List<long> MEDICINE_IDs { get; set; }
        public List<long> PREPARATION_MEDICINE_IDs { get; set; }

        public HisMedicineMedicineFilter()
            : base()
        {
        }
    }
}
