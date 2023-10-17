using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestVitaminASDO
    {
        public long? ExpMestId { get; set; }
        public List<long> VitaminAIds { get; set; }
        public long MediStockId { get; set; }
        public long ReqRoomId { get; set; }
        public string Description { get; set; }
    }
}
