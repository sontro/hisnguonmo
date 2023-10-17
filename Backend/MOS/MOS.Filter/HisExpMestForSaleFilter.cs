using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisExpMestForSaleFilter
    {
        public string SERVICE_REQ_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public List<long> SERVICE_REQ_IDs { get; set; }
    }
}
