using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class TransactionInfoSDO
    {
        /// <summary>
        /// So tien da tam ung
        /// </summary>
        public decimal TOTAL_DEPOSIT_AMOUNT { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }

        /// <summary>
        /// So tien benh nhan phai dua
        /// </summary>
        public decimal MUST_PAY_AMOUNT { get; set; }

        /// <summary>
        /// So tien Bien lai
        /// </summary>
        public decimal RECIEPT_AMOUNT { get; set; }

        /// <summary>
        /// So tien hoa don
        /// </summary>
        public decimal INVOICE_AMOUNT { get; set; }


        public decimal? AMOUNT_AN_BH { get; set; }
        public decimal? AMOUNT_AN_VP { get; set; }
        public decimal? AMOUNT_AN_DV { get; set; }

        public decimal? AMOUNT_CL_BH { get; set; }
        public decimal? AMOUNT_CL_VP { get; set; }
        public decimal? AMOUNT_CL_DV { get; set; }

        public decimal? AMOUNT_CN_BH { get; set; }
        public decimal? AMOUNT_CN_VP { get; set; }
        public decimal? AMOUNT_CN_DV { get; set; }

        public decimal? AMOUNT_GB_BH { get; set; }
        public decimal? AMOUNT_GB_VP { get; set; }
        public decimal? AMOUNT_GB_DV { get; set; }

        public decimal? AMOUNT_GI_BH { get; set; }
        public decimal? AMOUNT_GI_VP { get; set; }
        public decimal? AMOUNT_GI_DV { get; set; }

        public decimal? AMOUNT_HA_BH { get; set; }
        public decimal? AMOUNT_HA_VP { get; set; }
        public decimal? AMOUNT_HA_DV { get; set; }

        public decimal? AMOUNT_KH_BH { get; set; }
        public decimal? AMOUNT_KH_VP { get; set; }
        public decimal? AMOUNT_KH_DV { get; set; }

        public decimal? AMOUNT_MA_BH { get; set; }
        public decimal? AMOUNT_MA_VP { get; set; }
        public decimal? AMOUNT_MA_DV { get; set; }

        public decimal? AMOUNT_NS_BH { get; set; }
        public decimal? AMOUNT_NS_VP { get; set; }
        public decimal? AMOUNT_NS_DV { get; set; }

        public decimal? AMOUNT_PH_BH { get; set; }
        public decimal? AMOUNT_PH_VP { get; set; }
        public decimal? AMOUNT_PH_DV { get; set; }

        public decimal? AMOUNT_PT_BH { get; set; }
        public decimal? AMOUNT_PT_VP { get; set; }
        public decimal? AMOUNT_PT_DV { get; set; }

        public decimal? AMOUNT_SA_BH { get; set; }
        public decimal? AMOUNT_SA_VP { get; set; }
        public decimal? AMOUNT_SA_DV { get; set; }

        public decimal? AMOUNT_TH_BH { get; set; }
        public decimal? AMOUNT_TH_VP { get; set; }
        public decimal? AMOUNT_TH_DV { get; set; }

        public decimal? AMOUNT_TT_BH { get; set; }
        public decimal? AMOUNT_TT_VP { get; set; }
        public decimal? AMOUNT_TT_DV { get; set; }

        public decimal? AMOUNT_VT_BH { get; set; }
        public decimal? AMOUNT_VT_VP { get; set; }
        public decimal? AMOUNT_VT_DV { get; set; }

        public decimal? AMOUNT_XN_BH { get; set; }
        public decimal? AMOUNT_XN_VP { get; set; }
        public decimal? AMOUNT_XN_DV { get; set; }

        /// <summary>
        /// Tong tien ngoai tru da thanh toan trong cac giao dich truoc (Dua vao TREATMENT_TYPE_ID trong transaction
        /// </summary>
        public decimal? TOTAL_PRE_BILL_AMOUNT_OUT { get; set; }

        /// <summary>
        /// Tong tien BH chi tra cua cac dich vu da duoc thanh toan trong cac giao dich truoc
        /// </summary>
        public decimal? TOTAL_PRE_HEIN_PRICE { get; set; }

        /// <summary>
        /// Tong tien BH chi tra (ngoai tru) cua cac dich vu da duoc thanh toan trong cac giao dich truoc (Dua theo TREATMENT_TYPE_ID trong transaction)
        /// </summary>
        public decimal? TOTAL_PRE_HEIN_PRICE_OUT { get; set; }

        /// <summary>
        /// Tong tien BH chi tra cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// </summary>
        public decimal? TOTAL_HEIN_PRICE { get; set; }

        /// <summary>
        /// Tong tien BN chi tra cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// </summary>
        public decimal? TOTAL_PATIENT_PRICE { get; set; }

        /// <summary>
        /// Tong tien BN cung chi tra cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// </summary>
        public decimal? TOTAL_PATIENT_PRICE_BHYT { get; set; }

        /// <summary>
        /// Tong tien vien phi cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// 1. DTTT VP - DTPT Khong co
        /// 2. DTTT VP - DTPT Co: Lay chenh lech
        /// 2. DTT BHYT - DTPT VP: Lay chenh lech
        /// </summary>
        public decimal? TOTAL_PATIENT_PRICE_FEE { get; set; }

        /// <summary>
        /// Tong tien khac cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// 1. DTTT khong phai la BHYT va VP
        /// 2. DTTT BHYT, VP - DTPT Khong phai la BHYT va VP: Lay chenh lech
        /// </summary>
        public decimal? TOTAL_PATIENT_PRICE_OTHER { get; set; }

        /// <summary>
        /// Tong tien BN tu tra cua cac dich vu duoc thanh toan trong giao dich hien tai
        /// DTTT BHYT va co Tran
        /// </summary>
        public decimal? TOTAL_PATIENT_PRICE_DIFF { get; set; }
    }
}
