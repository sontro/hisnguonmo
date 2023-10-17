
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMetyMetyViewFilter : FilterBase
    {
        public long? MEDICINE_TYPE_ID { get; set; }
        public long? PREPARATION_MEDICINE_TYPE_ID { get; set; }
        public long? METY_PRODUCT_ID { get; set; }


        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> PREPARATION_MEDICINE_TYPE_IDs { get; set; }
        public List<long> METY_PRODUCT_IDs { get; set; }


        public HisMetyMetyViewFilter()
            : base()
        {
        }
    }
}
