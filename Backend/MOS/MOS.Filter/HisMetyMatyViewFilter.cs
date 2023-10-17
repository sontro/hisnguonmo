
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMetyMatyViewFilter : FilterBase
    {
        public long? MEDICINE_TYPE_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }
        public long? METY_PRODUCT_ID { get; set; }

        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> MATERIAL_TYPE_IDs { get; set; }
        public List<long> METY_PRODUCT_IDs { get; set; }

        public HisMetyMatyViewFilter()
            : base()
        {
        }
    }
}
