
using System;
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisSubclinicalRsAddFilter : FilterBase
    {
        public Nullable<long> REQUEST_ROOM_ID { get; set; }
        public Nullable<long> EXECUTE_ROOM_ID { get; set; }
        public Nullable<long> RESULT_ROOM_ID { get; set; }
        public Nullable<long> RESULT_DESK_ID { get; set; }

        public List<long> REQUEST_ROOM_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> RESULT_ROOM_IDs { get; set; }
        public List<long> RESULT_DESK_IDs { get; set; }

        public HisSubclinicalRsAddFilter()
            : base()
        {
        }
    }
}
