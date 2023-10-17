using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisTransactionRepaySDO
    {
        //Thong tin transaction
        public HIS_TRANSACTION Transaction { get; set; }
        //Danh sach tam ung can hoan ung
        public List<long> SereServDepositIds { get; set; }
        public long RequestRoomId { get; set; }
    }
}
