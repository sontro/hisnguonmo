using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestAggrSDO
    {
        public long ReqRoomId { get; set; }
        public string Description { get; set; }
        public List<long> ExpMestIds { get; set; }
    }
}
