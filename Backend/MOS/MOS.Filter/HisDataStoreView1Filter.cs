
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisDataStoreView1Filter : FilterBase
    {
        //Neu IsTree = true thi moi thuc hien tim kiem theo "node" (parent_id == node)
        public bool? IsTree { get; set; }
        public long? Node { get; set; }
        public long? BRANCH_ID { get; set; }

        public List<long> BRANCH_IDs { get; set; }

        public HisDataStoreView1Filter()
            : base()
        {
        }
    }
}
