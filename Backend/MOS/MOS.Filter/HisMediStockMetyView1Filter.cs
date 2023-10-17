using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisMediStockMetyView1Filter: FilterBase
    {
        public long? MEDI_STOCK_ID { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }

        public HisMediStockMetyView1Filter()
            : base()
        {
        }
    }
}
