using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisSereServTeinFilter : FilterBase
    {
        public List<long> SERE_SERV_IDs { get; set; }
        public List<long> TDL_TREATMENT_IDs { get; set; }
        public List<long> TEST_INDEX_IDs { get; set; }
        public List<long> TDL_SERVICE_REQ_IDs { get; set; }

        public long? SERE_SERV_ID { get; set; }
        public long? TEST_INDEX_ID { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }
        public long? TDL_SERVICE_REQ_ID { get; set; }

        public bool? HAS_VALUE { get; set; }

        public HisSereServTeinFilter()
            : base()
        {

        }
    }
}
