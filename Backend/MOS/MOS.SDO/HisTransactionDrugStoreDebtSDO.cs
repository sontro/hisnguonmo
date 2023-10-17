using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisTransactionDrugStoreDebtSDO
    {
        //Thong tin transaction
        public HIS_TRANSACTION Transaction { get; set; }

        /// <summary>
        /// Danh sach cac phieu xuat can chot no
        /// </summary>
        public List<long> ExpMestIds { get; set; }
        public long RequestRoomId { get; set; }
    }
}
