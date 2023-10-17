using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestTypeFilter : FilterBase
    {
        public string EXP_MEST_TYPE_CODE { get; set; }
        public List<string> EXP_MEST_TYPE_CODEs { get; set; }

        public HisExpMestTypeFilter()
            : base()
        {
        }
    }
}
