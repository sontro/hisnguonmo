using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisTransactionDebtSDO
    {
        //Thong tin transaction
        public HIS_TRANSACTION Transaction { get; set; }

        //Danh sach cac thong tin no cua yeu cau dich vu
        public List<HIS_SERE_SERV_DEBT> SereServDebts { get; set; }
        public long RequestRoomId { get; set; }
    }
}
