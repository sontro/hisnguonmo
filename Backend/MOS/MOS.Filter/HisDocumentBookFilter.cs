
namespace MOS.Filter
{
    public class HisDocumentBookFilter : FilterBase
    {
        public string DOCUMENT_BOOK_CODE__EXACT { get; set; }

        public bool? FOR_SICK_BHXH { get; set; }

        public HisDocumentBookFilter()
            : base()
        {
        }
    }
}
