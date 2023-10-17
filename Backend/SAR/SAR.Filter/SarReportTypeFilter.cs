
using System.Collections.Generic;
namespace SAR.Filter
{
    public class SarReportTypeFilter : FilterBase
    {
        public string REPORT_TYPE_CODE { get; set; }
        public long? REPORT_TYPE_GROUP_ID { get; set; }
        public List<long> REPORT_TYPE_GROUP_IDs { get; set; }

        public SarReportTypeFilter()
            : base()
        {
        }
    }
}
