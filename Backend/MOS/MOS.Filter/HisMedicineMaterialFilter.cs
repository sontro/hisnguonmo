
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineMaterialFilter : FilterBase
    {
        public long? MEDICINE_ID { get; set; }
        public long? MATERIAL_ID { get; set; }

        public List<long> MEDICINE_IDs { get; set; }
        public List<long> MATERIAL_IDs { get; set; }

        public HisMedicineMaterialFilter()
            : base()
        {
        }
    }
}
