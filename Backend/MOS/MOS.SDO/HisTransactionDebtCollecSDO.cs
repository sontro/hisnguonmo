using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisTransactionDebtCollecSDO
    {
        public long? TreatmentId { get; set; }
        public long AccountBookId { get; set; }
        public long PayFormId { get; set; }
        public long TransactionTime { get; set; }
        /// <summary>
        /// Can truyen trong truong hop so ko tu dong sinh so chung tu
        /// </summary>
        public long? NumOrder { get; set; }
        public string Description { get; set; }
        public decimal PayAmount { get; set; }
        public long RequestRoomId { get; set; }
        /// <summary>
        /// Can truyen trong truong hop cho hinh thuc giao dich la "tien mat/chuyen khoan"
        /// </summary>
        public decimal? TransferAmount { get; set; }

        /// <summary>
        /// Can truyen trong truong hop cho hinh thuc giao dich la "Tien mat/Quet the"
        /// </summary>
        public decimal? SwipeAmount { get; set; }

        /// <summary>
        /// So tien mien giam
        /// </summary>
        public decimal? Exemption { get; set; }

        /// <summary>
        /// Transaction_id cua cac giao dich cong no
        /// </summary>
        public List<long> DebtIds { get; set; }

    }
}
