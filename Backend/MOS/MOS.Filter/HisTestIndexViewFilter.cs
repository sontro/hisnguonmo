
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTestIndexViewFilter : FilterBase
    {
        public List<long> SERVICE_IDs { get; set; }
        public List<string> SERVICE_CODEs { get; set; }

        public HisTestIndexViewFilter()
            : base()
        {
        }
    }
}
