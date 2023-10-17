
namespace SAR.Filter
{
    public class SarRetyFofiViewFilter : FilterBase
    {
        public string DESCRIPTION { get; set; }
        public string JSON_OUTPUT { get; set; }
        public string REPORT_TYPE_CODE { get; set; }
        public string REPORT_TYPE_NAME { get; set; }
        public string FORM_FIELD_CODE { get; set; }
        public long? REPORT_TYPE_ID { get; set; }

        public SarRetyFofiViewFilter()
            : base()
        {
        }
    }
}
