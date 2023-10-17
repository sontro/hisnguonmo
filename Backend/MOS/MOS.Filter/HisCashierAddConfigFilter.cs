
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisCashierAddConfigFilter : FilterBase
    {
        public long? CASHIER_ROOM_ID { get; set; }
        public long? EXECUTE_ROOM_ID { get; set; }
        public long? REQUEST_ROOM_ID { get; set; }

        public List<long> CASHIER_ROOM_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> REQUEST_ROOM_IDs { get; set; }

        public HisCashierAddConfigFilter()
            : base()
        {
        }
    }
}
