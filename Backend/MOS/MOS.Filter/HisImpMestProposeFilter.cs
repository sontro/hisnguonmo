
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisImpMestProposeFilter : FilterBase
    {
        public List<long> PROPOSE_ROOM_IDs { get; set; }
        public List<long> PROPOSE_DEPARTMENT_IDs { get; set; }
        public List<long> SUPPLIER_IDs { get; set; }

        public string IMP_MEST_PROPOSE_CODE__EXACT { get; set; }
        public long? PROPOSE_ROOM_ID { get; set; }
        public long? PROPOSE_DEPARTMENT_ID { get; set; }
        public long? SUPPLIER_ID { get; set; }

        public HisImpMestProposeFilter()
            : base()
        {
        }
    }
}
