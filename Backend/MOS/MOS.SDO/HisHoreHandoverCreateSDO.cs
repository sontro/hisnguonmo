using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisHoreHandoverCreateSDO
    {
        public long? Id { get; set; }//Danh cho truong hop Sua phieu bang giao
        public long WorkingRoomId { get; set; }
        public long ReceiveRoomId { get; set; }
        public List<long> HisHoldReturnIds { get; set; }
    }
}
