
namespace SAR.Filter
{
    public class SarUserReportTypeViewFilter : FilterBase
    {
        public string LOGINNAME { get; set; }
        public long? REPORT_TYPE_ID { get; set; }
        public SarUserReportTypeViewFilter()
            : base()
        {
        }
    }
}
