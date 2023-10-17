
using System.Collections.Generic;
namespace SAR.Filter
{
    public class SarPrintFilter : FilterBase
    {
        public long? PRINT_TYPE_ID { get; set; }

        public List<long> PRINT_TYPE_IDs { get; set; }
        public SarPrintFilter()
            : base()
        {
        }
    }
}
