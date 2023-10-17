
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisServiceReqMaxNumOrderFilter
    {
        public long INSTRUCTION_DATE { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public bool? IS_PRIORITY { get; set; }
        public List<long> SERVICE_REQ_STT_IDs { get; set; }

        public HisServiceReqMaxNumOrderFilter()
            : base()
        {
        }
    }
}
