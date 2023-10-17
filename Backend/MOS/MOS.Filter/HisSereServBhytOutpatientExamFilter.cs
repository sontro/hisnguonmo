using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisSereServBhytOutpatientExamFilter : FilterBase
    {
        public long INTRUCTION_DATE { get; set; }
        public List<long> ROOM_IDs { get; set; }

        public HisSereServBhytOutpatientExamFilter()
            : base()
        {

        }
    }
}
