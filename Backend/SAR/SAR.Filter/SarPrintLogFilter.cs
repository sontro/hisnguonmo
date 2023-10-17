
namespace SAR.Filter
{
    public class SarPrintLogFilter : FilterBase
    {
        public string PRINT_TYPE_CODE__EXACT { get; set; }
        public string UNIQUE_CODE__EXACT { get; set; }
        public long? PRINT_TIME_FROM { get; set; }
        public long? PRINT_TIME_TO { get; set; }

        public SarPrintLogFilter()
            : base()
        {
        }
    }
}
