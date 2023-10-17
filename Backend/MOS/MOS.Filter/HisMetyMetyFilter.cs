
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMetyMetyFilter : FilterBase
    {

        public long? METY_PRODUCT_ID { get; set; }
        public long? PREPARATION_MEDICINE_TYPE_ID { get; set; }


        public List<long> METY_PRODUCT_IDs { get; set; }
        public List<long> PREPARATION_MEDICINE_TYPE_IDs { get; set; }

        public HisMetyMetyFilter()
            : base()
        {
        }
    }
}
