using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisCarerCardBorrowGiveBackSDO
    {
        public long CarerCardBorrowId { get; set; }
        public string ReceivingLoginName { get; set; }
        public string ReceivingUserName { get; set; }
        public long GiveBackTime { get; set; }
        public long RequestRoomId { get; set; }  // Phong dang lam viec
    }
}
