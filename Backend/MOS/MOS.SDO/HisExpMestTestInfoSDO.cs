using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestTestInfoSDO
    {
        public long ExpMestId { get; set; }
        public long RequestRoomId { get; set; }
        public List<ExpTestBloodSDO> ExpMestBloods { get; set; }
    }
}
