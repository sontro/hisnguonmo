
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisBedRoomView1Filter : FilterBase
    {
        public long? DEPARTMENT_ID { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }

        public HisBedRoomView1Filter()
            : base()
        {
        }
    }
}
