
namespace SAR.Filter
{
    public class SarUserReportTypeFilter : FilterBase
    {
        public string LOGINNAME { get; set; }     
        public long? REPORT_TYPE_ID { get; set; }
        public SarUserReportTypeFilter()
            : base()
        {
        }
    }
}
