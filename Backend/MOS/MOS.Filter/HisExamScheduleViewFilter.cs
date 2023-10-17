
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisExamScheduleViewFilter : FilterBase
    {
        public long? DAY_OF_WEEK__EQUAL { get; set; }
        public long? ROOM_ID { get; set; }

        public List<long> ROOM_IDs { get; set; }

        public HisExamScheduleViewFilter()
            : base()
        {
        }
    }
}
