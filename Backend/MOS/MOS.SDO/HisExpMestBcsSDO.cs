using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestBcsSDO
    {
        public long MediStockId { get; set; }
        public long ImpMediStockId { get; set; }
        public long ReqRoomId { get; set; }
        public string Description { get; set; }
        public List<long> ExpMestDttIds { get; set; }
        public List<long> ExpMestBcsIds { get; set; }
        public long? ExpMestReasonId { get; set; }
    }
}
