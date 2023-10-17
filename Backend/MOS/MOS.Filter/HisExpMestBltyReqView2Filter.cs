using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisExpMestBltyReqView2Filter : FilterBase
    {
        public long? EXP_MEST_ID { get; set; }
        public List<long> EXP_MEST_IDs { get; set; }
        public long? BLOOD_TYPE_ID { get; set; }
        public List<long> BLOOD_TYPE_IDs { get; set; }
        public long? TRACKING_ID { get; set; }
        public List<long> TRACKING_IDs { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }
        public long? INTRUCTION_TIME_FROM { get; set; }
        public long? INTRUCTION_TIME_TO { get; set; }
        public List<long> EXP_MEST_STT_IDs { get; set; }

        public HisExpMestBltyReqView2Filter()
            : base()
        {
        }
    }
}
