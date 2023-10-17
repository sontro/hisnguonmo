using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class AuthorityAccountBookSDO
    {
        public long? ApprovalTime { get; set; }
        public long? AccountBookId { get; set; }
        /// <summary>
        /// Room_id phong lam viec cua thu ngan
        /// </summary>
        public long? CashierWorkingRoomId { get; set; }
        public string CashierLoginName { get; set; }
        public string CashierUserName { get; set; }
        
        public long RequestTime { get; set; }
        public long RequestRoomId { get; set; }
        public string RequestLoginName { get; set; }
        public string RequestUserName { get; set; }
    }
}
