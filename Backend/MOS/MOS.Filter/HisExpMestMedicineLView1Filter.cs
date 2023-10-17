using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestMedicineLView1Filter
    {
        public long? INTRUCTION_DATE { get; set; }
        public long? TDL_PATIENT_ID { get; set; }
        public long? TDL_SERVICE_REQ_ID__NOT_EQUAL { get; set; }

        public HisExpMestMedicineLView1Filter()
            : base()
        {
        }
    }
}
