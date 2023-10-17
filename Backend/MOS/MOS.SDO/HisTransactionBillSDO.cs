using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisTransactionBillSDO
    {
        //Thong tin transaction
        public HIS_TRANSACTION Transaction { get; set; }
        //So tien can thu
        public decimal PayAmount { get; set; }
        //Danh sach cac thong tin can thanh toan cua yeu cau dich vu
        public List<HIS_SERE_SERV_BILL> SereServBills { get; set; }
        public long RequestRoomId { get; set; }
        public bool IsAutoRepay { get; set; }
        public long? RepayAccountBookId { get; set; }
        public long? RepayNumOrder { get; set; }
        public long? TreatmentId { get; set; }
        public long? OriginalTransactionId { get; set; } // ID giao dich goc bi huy
        public string ReplaceReason { get; set; } //Ly do thay the
        public long PayFormId { get; set; }
        public string TigTransactionCode { get; set; }
        public long? TigTransactionTime { get; set; }
        public string CardCode { get; set; }
        public decimal? RepayAmount { get; set; }
        public long TransactionTime { get; set; }
    }
}
