using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestConfirmSDO
    {
        public long ExpMestId { get; set; }
        public long ReqRoomId { get; set; }
        public List<ExpMestBltyReqSDO> ExpBltyReqs { get; set; }
    }
}
