
namespace SAR.Filter
{
    public class SarPrintTypeFilter : FilterBase
    {
        public string PRINT_TYPE_CODE { get; set; }
        public string FILE_PATTERN { get; set; }
        public short? HAS_PRINT_EXCEL { get; set; }
        public short? HAS_PRINT_WORD { get; set; }
        public string EMR_DOCUMENT_TYPE_CODE__EXACT { get; set; }

        public SarPrintTypeFilter()
            : base()
        {
        }
    }
}
