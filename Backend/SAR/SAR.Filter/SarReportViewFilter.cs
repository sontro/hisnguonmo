using System.Collections.Generic;

namespace SAR.Filter
{
    public class SarReportViewFilter : FilterBase
    {
        public long? REPORT_STT_ID { get; set; }
        public List<long> REPORT_STT_IDs { get; set; }
        public short? IS_REFERENCE_TESTING { get; set; }
        public bool? IS_EXCLUDE_FILTER_BY_LOGINNAME { get; set; }

        public enum FilterModeEnum
        {
            ALL,
            CREATE,
            RECEIVE,
        }
        public FilterModeEnum FilterMode { get; set; }

        public SarReportViewFilter()
            : base()
        {
        }
    }
}
