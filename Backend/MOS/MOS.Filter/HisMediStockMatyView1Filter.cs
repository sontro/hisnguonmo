using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisMediStockMatyView1Filter: FilterBase
    {
        public long? MEDI_STOCK_ID { get; set; }
        public List<long> MATERIAL_TYPE_IDs { get; set; }

        public HisMediStockMatyView1Filter()
            : base()
        {
        }
    }
}
