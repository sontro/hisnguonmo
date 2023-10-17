using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisSereServTeinView1Filter : FilterBase
    {
        public List<long> TEST_INDEX_IDs { get; set; }
        public List<long> TREATMENT_IDs { get; set; }

        public HisSereServTeinView1Filter()
            : base()
        {
        }
    }
}
