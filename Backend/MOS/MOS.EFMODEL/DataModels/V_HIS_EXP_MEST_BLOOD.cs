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
    
    public partial class V_HIS_EXP_MEST_BLOOD
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
        public long EXP_MEST_ID { get; set; }
        public long BLOOD_ID { get; set; }
        public long TDL_MEDI_STOCK_ID { get; set; }
        public Nullable<long> MEDI_STOCK_PERIOD_ID { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public Nullable<decimal> VAT_RATIO { get; set; }
        public Nullable<decimal> DISCOUNT { get; set; }
        public Nullable<long> NUM_ORDER { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<short> IS_EXPORT { get; set; }
        public Nullable<short> IS_TH { get; set; }
        public string APPROVAL_LOGINNAME { get; set; }
        public string APPROVAL_USERNAME { get; set; }
        public Nullable<long> APPROVAL_TIME { get; set; }
        public Nullable<long> APPROVAL_DATE { get; set; }
        public string EXP_LOGINNAME { get; set; }
        public string EXP_USERNAME { get; set; }
        public Nullable<long> EXP_TIME { get; set; }
        public Nullable<long> EXP_DATE { get; set; }
        public long TDL_BLOOD_TYPE_ID { get; set; }
        public Nullable<long> EXP_MEST_BLTY_REQ_ID { get; set; }
        public Nullable<long> PATIENT_TYPE_ID { get; set; }
        public Nullable<long> SERE_SERV_PARENT_ID { get; set; }
        public Nullable<short> IS_OUT_PARENT_FEE { get; set; }
        public Nullable<long> TDL_SERVICE_REQ_ID { get; set; }
        public Nullable<long> TDL_TREATMENT_ID { get; set; }
        public Nullable<decimal> VIR_PRICE { get; set; }
        public string PATIENT_BLOOD_ABO_CODE { get; set; }
        public string PATIENT_BLOOD_RH_CODE { get; set; }
        public string PUC { get; set; }
        public string TEST_TUBE { get; set; }
        public string SCANGEL_GELCARD { get; set; }
        public string COOMBS { get; set; }
        public Nullable<long> SALT_ENVI { get; set; }
        public Nullable<long> ANTI_GLOBULIN_ENVI { get; set; }
        public string TEST_TUBE_TWO { get; set; }
        public Nullable<long> SALT_ENVI_TWO { get; set; }
        public Nullable<long> ANTI_GLOBULIN_ENVI_TWO { get; set; }
        public Nullable<decimal> AC_SELF_ENVIDENCE { get; set; }
        public Nullable<decimal> AC_SELF_ENVIDENCE_SECOND { get; set; }
        public string EXP_MEST_CODE { get; set; }
        public long EXP_MEST_TYPE_ID { get; set; }
        public Nullable<long> AGGR_EXP_MEST_ID { get; set; }
        public long MEDI_STOCK_ID { get; set; }
        public long EXP_MEST_STT_ID { get; set; }
        public long REQ_ROOM_ID { get; set; }
        public long REQ_DEPARTMENT_ID { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public string TDL_TREATMENT_CODE { get; set; }
        public string BLOOD_CODE { get; set; }
        public long BLOOD_TYPE_ID { get; set; }
        public long BLOOD_ABO_ID { get; set; }
        public Nullable<long> BLOOD_RH_ID { get; set; }
        public decimal IMP_PRICE { get; set; }
        public decimal IMP_VAT_RATIO { get; set; }
        public Nullable<decimal> INTERNAL_PRICE { get; set; }
        public Nullable<long> IMP_TIME { get; set; }
        public string PACKAGE_NUMBER { get; set; }
        public Nullable<long> EXPIRED_DATE { get; set; }
        public string BID_NUM_ORDER { get; set; }
        public Nullable<long> BID_ID { get; set; }
        public Nullable<long> IMP_SOURCE_ID { get; set; }
        public Nullable<long> SUPPLIER_ID { get; set; }
        public string GIVE_NAME { get; set; }
        public string GIVE_CODE { get; set; }
        public Nullable<long> PACKING_TIME { get; set; }
        public string BLOOD_TYPE_CODE { get; set; }
        public string BLOOD_TYPE_NAME { get; set; }
        public long SERVICE_ID { get; set; }
        public Nullable<long> BLOOD_TYPE_NUM_ORDER { get; set; }
        public long BLOOD_VOLUME_ID { get; set; }
        public string ELEMENT { get; set; }
        public long SERVICE_UNIT_ID { get; set; }
        public decimal VOLUME { get; set; }
        public string BLOOD_ABO_CODE { get; set; }
        public string BLOOD_RH_CODE { get; set; }
        public string SERVICE_UNIT_CODE { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string BID_NUMBER { get; set; }
        public string BID_NAME { get; set; }
        public string IMP_SOURCE_CODE { get; set; }
        public string IMP_SOURCE_NAME { get; set; }
        public string SUPPLIER_CODE { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string PATIENT_TYPE_CODE { get; set; }
        public string PATIENT_TYPE_NAME { get; set; }
    }
}
