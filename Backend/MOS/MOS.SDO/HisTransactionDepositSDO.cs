using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisTransactionDepositSDO
    {
        public long RequestRoomId { get; set; }
        public long? DepositReqId { get; set; }
        public bool? IsCollected { get; set; }
        public string LastDigitsOfBankCardCode { get; set; }
        //Trong truong hop quet the de thanh toan thi can gui len so the
        public string CardCode { get; set; }

        //Thong tin transaction
        public HIS_TRANSACTION Transaction { get; set; }
        //Danh sach cac sere_serv tuong ung voi giao dich tam ung
        public List<HIS_SERE_SERV_DEPOSIT> SereServDeposits { get; set; }
    }
}
