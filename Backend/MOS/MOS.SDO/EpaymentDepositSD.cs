using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class EpaymentDepositSD
    {
        /// <summary>
        /// Can truyen trong truong hop thanh toan co thuc hien quet the (vd: kiosk)
        /// </summary>
        public string CardServiceCode { get; set; }
        public long RequestRoomId { get; set; }
        public List<long> ServiceReqIds { get; set; }
        public bool IncludeAttachment { get; set; }
    }
}
