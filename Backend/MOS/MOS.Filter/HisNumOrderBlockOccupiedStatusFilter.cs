using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisNumOrderBlockOccupiedStatusFilter
    {
        public long ROOM_ID { get; set; }
        public long ISSUE_DATE { get; set; }
        public long? NUM_ORDER_BLOCK_ID { get; set; }
        public string FROM_TIME__FROM { get; set; }
        public string TO_TIME__FROM { get; set; }

        public HisNumOrderBlockOccupiedStatusFilter()
            : base()
        {
        }
    }
}