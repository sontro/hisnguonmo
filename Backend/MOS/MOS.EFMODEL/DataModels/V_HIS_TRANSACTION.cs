//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MOS.EFMODEL.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_HIS_TRANSACTION
    {
        public long ID { get; set; }
        public Nullable<long> CREATE_TIME { get; set; }
        public Nullable<long> MODIFY_TIME { get; set; }
        public string CREATOR { get; set; }
        public string MODIFIER { get; set; }
        public string APP_CREATOR { get; set; }
        public string APP_MODIFIER { get; set; }
        public Nullable<short> IS_ACTIVE { get; set; }
        public Nullable<short> IS_DELETE { get; set; }
        public string GROUP_CODE { get; set; }
        public string TRANSACTION_CODE { get; set; }
        public long TRANSACTION_TYPE_ID { get; set; }
        public long TRANSACTION_TIME { get; set; }
        public long TRANSACTION_DATE { get; set; }
        public decimal AMOUNT { get; set; }
        public long NUM_ORDER { get; set; }
        public long ACCOUNT_BOOK_ID { get; set; }
        public long PAY_FORM_ID { get; set; }
        public Nullable<long> REPAY_REASON_ID { get; set; }
        public long CASHIER_ROOM_ID { get; set; }
        public string CASHIER_LOGINNAME { get; set; }
        public string CASHIER_USERNAME { get; set; }
        public Nullable<decimal> KC_AMOUNT { get; set; }
        public Nullable<decimal> TDL_BILL_FUND_AMOUNT { get; set; }
        public Nullable<long> TDL_SERE_SERV_DEPOSIT_COUNT { get; set; }
        public Nullable<long> TDL_SESE_DEPO_REPAY_COUNT { get; set; }
        public Nullable<decimal> EXEMPTION { get; set; }
        public string EXEMPTION_REASON { get; set; }
        public string SELLER_NAME { get; set; }
        public string SELLER_ADDRESS { get; set; }
        public string SELLER_ACCOUNT_NUMBER { get; set; }
        public string SELLER_TAX_CODE { get; set; }
        public string SELLER_PHONE { get; set; }
        public string BUYER_NAME { get; set; }
        public string BUYER_ADDRESS { get; set; }
        public string BUYER_ACCOUNT_NUMBER { get; set; }
        public string BUYER_TAX_CODE { get; set; }
        public string FILE_URL { get; set; }
        public string FILE_NAME { get; set; }
        public Nullable<long> BILL_TYPE_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<short> IS_CANCEL { get; set; }
        public string CANCEL_REASON { get; set; }
        public Nullable<long> CANCEL_TIME { get; set; }
        public string CANCEL_LOGINNAME { get; set; }
        public string CANCEL_USERNAME { get; set; }
        public Nullable<long> CASHOUT_ID { get; set; }
        public string TIG_TRANSACTION_CODE { get; set; }
        public Nullable<long> TIG_TRANSACTION_TIME { get; set; }
        public string TIG_VOID_CODE { get; set; }
        public string INVOICE_SYS { get; set; }
        public string INVOICE_CODE { get; set; }
        public Nullable<long> TREATMENT_ID { get; set; }
        public Nullable<long> TREATMENT_TYPE_ID { get; set; }
        public string TDL_TREATMENT_CODE { get; set; }
        public Nullable<long> TDL_PATIENT_ID { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public string TDL_PATIENT_FIRST_NAME { get; set; }
        public string TDL_PATIENT_LAST_NAME { get; set; }
        public Nullable<long> TDL_PATIENT_DOB { get; set; }
        public Nullable<short> TDL_PATIENT_IS_HAS_NOT_DAY_DOB { get; set; }
        public string TDL_PATIENT_ADDRESS { get; set; }
        public Nullable<long> TDL_PATIENT_GENDER_ID { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public string TDL_PATIENT_CAREER_NAME { get; set; }
        public string TDL_PATIENT_WORK_PLACE { get; set; }
        public string TDL_PATIENT_WORK_PLACE_NAME { get; set; }
        public string TDL_PATIENT_DISTRICT_CODE { get; set; }
        public string TDL_PATIENT_PROVINCE_CODE { get; set; }
        public string TDL_PATIENT_COMMUNE_CODE { get; set; }
        public string TDL_PATIENT_MILITARY_RANK_NAME { get; set; }
        public string TDL_PATIENT_NATIONAL_NAME { get; set; }
        public string NATIONAL_TRANSACTION_CODE { get; set; }
        public string BUYER_ORGANIZATION { get; set; }
        public Nullable<long> SALE_TYPE_ID { get; set; }
        public Nullable<decimal> TREATMENT_TOTAL_PRICE { get; set; }
        public Nullable<decimal> TREATMENT_MEDICINE_PRICE { get; set; }
        public Nullable<decimal> TREATMENT_MATERIAL_PRICE { get; set; }
        public Nullable<decimal> TREATMENT_SUBCLINICAL_PRICE { get; set; }
        public Nullable<decimal> TREATMENT_SURG_PRICE { get; set; }
        public Nullable<decimal> TREATMENT_EXAM_PRICE { get; set; }
        public Nullable<decimal> TREATMENT_BED_PRICE { get; set; }
        public Nullable<decimal> TREATMENT_DEPOSIT_AMOUNT { get; set; }
        public Nullable<decimal> TREATMENT_REPAY_AMOUNT { get; set; }
        public Nullable<decimal> TREATMENT_HEIN_PRICE { get; set; }
        public Nullable<decimal> TREATMENT_PATIENT_PRICE { get; set; }
        public Nullable<decimal> TREATMENT_BILL_AMOUNT { get; set; }
        public Nullable<decimal> TREATMENT_BLOOD_PRICE { get; set; }
        public string SESSION_CODE { get; set; }
        public string TRANSACTION_INFO { get; set; }
        public Nullable<long> CANCEL_CASHIER_ROOM_ID { get; set; }
        public string BUYER_PHONE { get; set; }
        public Nullable<short> IS_NOT_IN_WORKING_TIME { get; set; }
        public Nullable<decimal> TDL_PREVIOUS_AMOUNT { get; set; }
        public Nullable<long> PREVIOUS_ID { get; set; }
        public Nullable<decimal> TDL_BILL_AMOUNT { get; set; }
        public Nullable<decimal> TDL_PREVIOUS_BILL_AMOUNT { get; set; }
        public Nullable<long> BILL_ID { get; set; }
        public Nullable<decimal> SERE_SERV_AMOUNT { get; set; }
        public Nullable<decimal> TRANSFER_AMOUNT { get; set; }
        public Nullable<long> WORKING_SHIFT_ID { get; set; }
        public Nullable<long> DEBT_BILL_ID { get; set; }
        public Nullable<short> IS_DEBT_COLLECTION { get; set; }
        public string EINVOICE_NUM_ORDER { get; set; }
        public Nullable<long> TIG_VOID_TIME { get; set; }
        public Nullable<short> IS_DIRECTLY_BILLING { get; set; }
        public Nullable<long> DEBT_TYPE { get; set; }
        public Nullable<decimal> TREATMENT_DEBT_AMOUNT { get; set; }
        public Nullable<decimal> TREATMENT_TRANSFER_AMOUNT { get; set; }
        public Nullable<decimal> SWIPE_AMOUNT { get; set; }
        public string ALL_TRANS_CODES_IN_INVOICE { get; set; }
        public Nullable<long> EINVOICE_TIME { get; set; }
        public Nullable<long> TRANS_REQ_ID { get; set; }
        public Nullable<long> BANK_ID { get; set; }
        public Nullable<long> CANCEL_REASON_ID { get; set; }
        public Nullable<decimal> ROUNDED_TOTAL_PRICE { get; set; }
        public Nullable<decimal> ROUND_PRICE_BASE { get; set; }
        public string POS_INVOICE { get; set; }
        public string POS_PAN { get; set; }
        public string POS_CARD_HOLDER { get; set; }
        public string POS_RESULT_JSON { get; set; }
        public Nullable<short> BUYER_TYPE { get; set; }
        public Nullable<short> IS_CANCEL_EINVOICE { get; set; }
        public Nullable<long> EINVOICE_CANCEL_TIME { get; set; }
        public Nullable<long> BUYER_WORK_PLACE_ID { get; set; }
        public string EINVOICE_URL { get; set; }
        public Nullable<long> TDL_PATIENT_CLASSIFY_ID { get; set; }
        public string REPLACE_REASON { get; set; }
        public Nullable<long> REPLACE_TIME { get; set; }
        public Nullable<long> ORIGINAL_TRANSACTION_ID { get; set; }
        public string TDL_ORIGINAL_EI_NUM_ORDER { get; set; }
        public Nullable<long> TDL_ORIGINAL_EI_TIME { get; set; }
        public string TDL_ORIGINAL_EI_CODE { get; set; }
        public string EINVOICE_LOGINNAME { get; set; }
        public Nullable<long> CARD_ID { get; set; }
        public string TDL_CARD_CODE { get; set; }
        public string TDL_BANK_CARD_CODE { get; set; }
        public Nullable<short> CANCEL_REQ_STT { get; set; }
        public string CANCEL_REQ_REASON { get; set; }
        public Nullable<long> CANCEL_REQ_ROOM_ID { get; set; }
        public string CANCEL_REQ_LOGINNAME { get; set; }
        public string CANCEL_REQ_USERNAME { get; set; }
        public string CANCEL_REQ_REJECT_LOGINNAME { get; set; }
        public string CANCEL_REQ_REJECT_USERNAME { get; set; }
        public string CANCEL_REQ_REJECT_REASON { get; set; }
        public Nullable<long> CANCEL_REQ_REJECT_TIME { get; set; }
        public string BANK_TRANSACTION_CODE { get; set; }
        public Nullable<long> BANK_TRANSACTION_TIME { get; set; }
        public Nullable<long> UNLOCK_TIME { get; set; }
        public Nullable<long> BEFORE_UL_CASHIER_ROOM_ID { get; set; }
        public string BEFORE_UL_CASHIER_LOGINNAME { get; set; }
        public string BEFORE_UL_CASHIER_USERNAME { get; set; }
        public string TREATMENT_CODE { get; set; }
        public string TRANSACTION_TYPE_CODE { get; set; }
        public string TRANSACTION_TYPE_NAME { get; set; }
        public string PAY_FORM_CODE { get; set; }
        public string PAY_FORM_NAME { get; set; }
        public string ACCOUNT_BOOK_CODE { get; set; }
        public string ACCOUNT_BOOK_NAME { get; set; }
        public string SYMBOL_CODE { get; set; }
        public string TEMPLATE_CODE { get; set; }
        public Nullable<short> IS_NOT_GEN_TRANSACTION_ORDER { get; set; }
        public Nullable<long> EINVOICE_TYPE_ID { get; set; }
        public string CASHIER_ROOM_CODE { get; set; }
        public string CASHIER_ROOM_NAME { get; set; }
        public string TREATMENT_TYPE_CODE { get; set; }
        public string TREATMENT_TYPE_NAME { get; set; }
        public string REPAY_REASON_CODE { get; set; }
        public string REPAY_REASON_NAME { get; set; }
        public string CANCEL_CASHIER_ROOM_CODE { get; set; }
        public string CANCEL_CASHIER_ROOM_NAME { get; set; }
        public string BANK_CODE { get; set; }
        public string BANK_NAME { get; set; }
        public string TDL_PATIENT_CLASSIFY_CODE { get; set; }
        public string TDL_PATIENT_CLASSIFY_NAME { get; set; }
        public string CANCEL_REQ_DEPARTMENT_CODE { get; set; }
        public string CANCEL_REQ_DEPARTMENT_NAME { get; set; }
        public string CANCEL_REQ_ROOM_CODE { get; set; }
        public string CANCEL_REQ_ROOM_NAME { get; set; }
        public string TRANS_REQ_CODE { get; set; }
    }
}
