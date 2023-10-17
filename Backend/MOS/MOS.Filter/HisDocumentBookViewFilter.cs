
namespace MOS.Filter
{
    public class HisDocumentBookViewFilter : FilterBase
    {
        public string DOCUMENT_BOOK_CODE__EXACT { get; set; }

        public bool? IS_OUT_NUM_ORDER { get; set; }
        public bool? FOR_SICK_BHXH { get; set; }

        public HisDocumentBookViewFilter()
            : base()
        {
        }
    }
}
