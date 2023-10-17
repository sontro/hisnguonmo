
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisRoomCounterLViewFilter
    {
        protected static readonly long NEGATIVE_ID = -1;

        public List<long> IDs { get; set; }
        //Mang y nghia la lay ra cac phong co the xu ly duoc toan bo cac dich vu nay (theo cau hinh dich vu - phong)
        public List<long> SERVICE_IDs { get; set; }   
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<long> ROOM_IDs { get; set; }  

        public long? ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? AREA_ID { get; set; }
        public long? BRANCH_ID { get; set; }
        public bool? IS_EXAM { get; set; }
        public short? IS_ACTIVE { get; set; }
        public bool? IS_PAUSE_ENCLITIC { get; set; }
        public long? ID__NOT_EQUAL { get; set; }

        public string ORDER_FIELD { get; set; }
        public string ORDER_DIRECTION { get; set; }

        public HisRoomCounterLViewFilter()
            : base()
        {

        }
    }
}
