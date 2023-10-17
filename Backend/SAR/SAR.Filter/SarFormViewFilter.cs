
using System.Collections.Generic;
namespace SAR.Filter
{
    public class SarFormViewFilter : FilterBase
    {
        public long? FORM_TYPE_ID { get; set; }
        public List<long> FORM_TYPE_IDs { get; set; }
        public string FORM_TYPE_CODE__EXACT { get; set; }

        public SarFormViewFilter()
            : base()
        {
        }
    }
}
