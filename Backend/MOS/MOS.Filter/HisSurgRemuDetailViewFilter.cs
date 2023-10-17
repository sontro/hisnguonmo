
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisSurgRemuDetailViewFilter : FilterBase
    {
        public long? EXECUTE_ROLE_ID { get; set; }
        public long? SURG_REMUNERATION_ID { get; set; }
        public long? ID_NOT_EQUAL { get; set; }

        public List<long> EXECUTE_ROLE_IDs { get; set; }
        public List<long> SURG_REMUNERATION_IDs { get; set; }

        public HisSurgRemuDetailViewFilter()
            : base()
        {
        }
    }
}
