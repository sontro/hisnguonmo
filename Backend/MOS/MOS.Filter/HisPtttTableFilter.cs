
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPtttTableFilter : FilterBase
    {
        public long? EXECUTE_ROOM_ID { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }

        public HisPtttTableFilter()
            : base()
        {
        }
    }
}
