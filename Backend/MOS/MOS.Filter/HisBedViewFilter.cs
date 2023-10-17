
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBedViewFilter : FilterBase
    {
        public long? BED_ROOM_ID { get; set; }
        public List<long> BED_ROOM_IDs { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }

        public HisBedViewFilter()
            : base()
        {
        }
    }
}
