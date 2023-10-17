
using System.Collections.Generic;
namespace SAR.Filter
{
    public class SarFormDataFilter : FilterBase
    {
        public long? FORM_ID { get; set; }
        public List<long> FORM_IDs { get; set; }

        public SarFormDataFilter()
            : base()
        {
        }
    }
}
