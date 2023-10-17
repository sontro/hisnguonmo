
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMaterialView1Filter : FilterBase
    {
        public string BID_NUMBER__EXACT { get; set; }
        public string MATERIAL_TYPE_CODE__EXACT { get; set; }
        public long? BID_ID { get; set; }
        public List<long> BID_IDs { get; set; }
        public long? IMP_TIME_FROM { get; set; }
        public long? IMP_TIME_TO { get; set; }

        public string TDL_IMP_MEST_CODE { get; set; }
        public string TDL_IMP_MEST_SUB_CODE { get; set; }

        public bool? IS_BUSINESS { get; set; }
        public string HEIN_SERVICE_BHYT_NAME { get; set; }

        public HisMaterialView1Filter()
            : base()
        {
        }
    }
}
