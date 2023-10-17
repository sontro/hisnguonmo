using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class DHisTransExpSDO
    {
        public short? IS_ACTIVE { get; set; }
        public short? IS_CANCEL { get; set; }
        public long? CASHIER_ROOM_ID { get; set; }
        public string PAY_FORM_CODE { get; set; }
        public string PAY_FORM_NAME { get; set; }
        public string TYPE_NAME { get; set; }
        public long? TYPE_ID { get; set; }
        public string ROOM_CODE { get; set; }
        public string ROOM_NAME { get; set; }
        public string ACCOUNT_BOOK_CODE { get; set; }
        public string ACCOUNT_BOOK_NAME { get; set; }
        public string SYMBOL_CODE { get; set; }
        public string TEMPLATE_CODE { get; set; }
        public short? IS_NOT_GEN_TRANSACTION_ORDER { get; set; }
        public string CANCEL_CASHIER_ROOM_CODE { get; set; }
        public string CANCEL_CASHIER_ROOM_NAME { get; set; }
        public string EXP_MEST_CODE { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public short? TDL_PATIENT_IS_HAS_NOT_DAY_DOB { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public long? TDL_PATIENT_DOB { get; set; }
        public long? CREATE_TIME { get; set; }
        public string TRANSACTION_CODE { get; set; }
        public decimal? AMOUNT { get; set; }
        public decimal? KC_AMOUNT { get; set; }
        public decimal? TDL_BILL_FUND_AMOUNT { get; set; }
        public decimal? EXEMPTION { get; set; }
        public string EXEMPTION_REASON { get; set; }
        public long? PAY_FORM_ID { get; set; }
        public string CASHIER_LOGINNAME { get; set; }
        public string CASHIER_USERNAME { get; set; }
        public string TREATMENT_CODE { get; set; }
        public long? NUM_ORDER { get; set; }
        public string INVOICE_CODE { get; set; }
        public string INVOICE_SYS { get; set; }
        public string EINVOICE_NUM_ORDER { get; set; }
        public long? EINVOICE_TIME { get; set; }
        public long? EINVOICE_TYPE_ID { get; set; }
    }
}
