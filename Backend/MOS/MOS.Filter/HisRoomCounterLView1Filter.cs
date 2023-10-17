
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRoomCounterLView1Filter
    {
        protected static readonly long NEGATIVE_ID = -1;

        public List<long> IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public long? ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? BRANCH_ID { get; set; }
        public bool? IS_EXAM { get; set; }
        public short? IS_ACTIVE { get; set; }

        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }

        public HisRoomCounterLView1Filter()
            : base()
        {

        }
    }
}
