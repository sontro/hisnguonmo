using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisSereServPtttView1Filter : FilterBase
    {
        public List<long> SERE_SERV_IDs { get; set; }
        public List<long> PTTT_GROUP_IDs { get; set; }

        public long? SERE_SERV_ID { get; set; }
        public long? PTTT_GROUP_ID { get; set; }

        public long? TDL_TREATMENT_ID { get; set; }
        public List<long> TDL_TREATMENT_IDs { get; set; }

        public long? SERVICE_TYPE_ID { get; set; }
        public string SERVICE_TYPE_CODE__EXACT { get; set; }

        public HisSereServPtttView1Filter()
            : base()
        {
        }
    }
}
