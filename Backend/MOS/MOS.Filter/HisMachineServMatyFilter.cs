
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMachineServMatyFilter : FilterBase
    {
        public long? MACHINE_ID { get; set; }
        public long? SERVICE_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }

        public List<long> MACHINE_IDs { get; set; }
        public List<long> SERVICE_IDs { get; set; }
        public List<long> MATERIAL_TYPE_IDs { get; set; }

        public HisMachineServMatyFilter()
            : base()
        {
        }
    }
}
