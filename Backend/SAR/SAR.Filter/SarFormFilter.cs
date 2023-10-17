
using System.Collections.Generic;
namespace SAR.Filter
{
    public class SarFormFilter : FilterBase
    {
        public long? FORM_TYPE_ID { get; set; }
        public List<long> FORM_TYPE_IDs { get; set; }

        public SarFormFilter()
            : base()
        {
        }
    }
}
