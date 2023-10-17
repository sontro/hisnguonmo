
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMatyMatyFilter : FilterBase
    {
        public long? MATERIAL_TYPE_ID { get; set; }
        public long? PREPARATION_MATERIAL_TYPE_ID { get; set; }


        public List<long> MATERIAL_TYPE_IDs { get; set; }
        public List<long> PREPARATION_MATERIAL_TYPE_IDs { get; set; }

        public HisMatyMatyFilter()
            : base()
        {
        }
    }
}
