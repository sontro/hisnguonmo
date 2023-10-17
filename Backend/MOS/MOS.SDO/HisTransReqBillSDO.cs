using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTransReqBillSDO
    {
        public HIS_TRANS_REQ TransReq { get; set; }
        public List<HIS_SESE_TRANS_REQ> SeseTransReqs { get; set; }
        public decimal PayAmount { get; set; }
        public long RequestRoomId { get; set; }
    }
}
