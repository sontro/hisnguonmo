using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqPlanSDO
    {
        public long ServiceReqId { get; set; }
        public long WorkingRoomId { get; set; }
        public long ExecuteRoomId { get; set; }
        public long? PlanTimeFrom { get; set; }
        public long? PlanTimeTo { get; set; }
        public long? PtttMethodId { get; set; }
        public long? EmotionlessMethodId { get; set; }
        public string PlanningRequest { get; set; }
        public string SurgeryNote { get; set; }
        public string Manner { get; set; }

        public List<EkipSDO> PlanEkip { get; set; }
    }
}