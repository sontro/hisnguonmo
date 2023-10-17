using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction
{
    class HisTransactionConstant
    {
        /// <summary>
        /// Tong tien ngoai tru da thanh toan trong cac giao dich truoc (Dua vao TREATMENT_TYPE_ID trong transaction
        /// </summary>
        public const string TOTAL_PRE_BILL_AMOUNT_OUT = "TOTAL_PRE_BILL_AMOUNT_OUT";

        /// <summary>
        /// Tong tien BH chi tra cua cac dich vu da duoc thanh toan trong cac giao dich truoc
        /// </summary>
        public const string TOTAL_PRE_HEIN_PRICE = "TOTAL_PRE_HEIN_PRICE";

        /// <summary>
        /// Tong tien BH chi tra (ngoai tru) cua cac dich vu da duoc thanh toan trong cac giao dich truoc (Dua theo TREATMENT_TYPE_ID trong transaction)
        /// </summary>
        public const string TOTAL_PRE_HEIN_PRICE_OUT = "TOTAL_PRE_HEIN_PRICE_OUT";

        /// <summary>
        /// Tong tien BH chi tra cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// </summary>
        public const string TOTAL_HEIN_PRICE = "TOTAL_HEIN_PRICE";

        /// <summary>
        /// Tong tien BN chi tra cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// </summary>
        public const string TOTAL_PATIENT_PRICE = "TOTAL_PATIENT_PRICE";

        /// <summary>
        /// Tong tien BN cung chi tra cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// </summary>
        public const string TOTAL_PATIENT_PRICE_BHYT = "TOTAL_PATIENT_PRICE_BHYT";

        /// <summary>
        /// Tong tien vien phi cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// </summary>
        public const string TOTAL_PATIENT_PRICE_FEE = "TOTAL_PATIENT_PRICE_FEE";

        /// <summary>
        /// Tong tien khac (Doi tuong thanh toan khac BHYT va VP) cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// </summary>
        public const string TOTAL_PATIENT_PRICE_OTHER = "TOTAL_PATIENT_PRICE_OTHER";

        /// <summary>
        /// Tong tien BN tu tra cua cac dich vu duoc thanh toan trong giao dich hien tai (Cac dich vu co Doi tuong thanh toan la BHYT va (khong co doi tuong phu thu hoac doi tuong phu thu la VP))
        /// </summary>
        public const string TOTAL_PATIENT_PRICE_DIFF = "TOTAL_PATIENT_PRICE_DIFF";
    }
}
