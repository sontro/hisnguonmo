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
    
    public partial class HIS_MEDI_CONTRACT_METY
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
        public long MEDICAL_CONTRACT_ID { get; set; }
        public long MEDICINE_TYPE_ID { get; set; }
        public Nullable<long> BID_MEDICINE_TYPE_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public Nullable<decimal> IMP_PRICE { get; set; }
        public Nullable<decimal> IMP_VAT_RATIO { get; set; }
        public Nullable<decimal> INTERNAL_PRICE { get; set; }
        public Nullable<long> EXPIRED_DATE { get; set; }
        public string NATIONAL_NAME { get; set; }
        public Nullable<long> MANUFACTURER_ID { get; set; }
        public string CONCENTRA { get; set; }
        public string MEDICINE_REGISTER_NUMBER { get; set; }
        public Nullable<long> YEAR_LIFESPAN { get; set; }
        public Nullable<long> MONTH_LIFESPAN { get; set; }
        public Nullable<long> DAY_LIFESPAN { get; set; }
        public Nullable<decimal> CONTRACT_PRICE { get; set; }
        public Nullable<long> IMP_EXPIRED_DATE { get; set; }
        public Nullable<long> HOUR_LIFESPAN { get; set; }
        public string ACTIVE_INGR_BHYT_NAME { get; set; }
        public Nullable<long> MEDICINE_USE_FORM_ID { get; set; }
        public string DOSAGE_FORM { get; set; }
        public Nullable<decimal> VIR_CONTRACT_PRICE { get; set; }
        public Nullable<decimal> VIR_BID_MEDICINE_TYPE_ID { get; set; }
        public string BID_NUMBER { get; set; }
        public string BID_GROUP_CODE { get; set; }
        public string NOTE { get; set; }
        public string VIR_BID_NUMBER { get; set; }
        public string VIR_BID_GROUP_CODE { get; set; }
    
        public virtual HIS_BID_MEDICINE_TYPE HIS_BID_MEDICINE_TYPE { get; set; }
        public virtual HIS_MANUFACTURER HIS_MANUFACTURER { get; set; }
        public virtual HIS_MEDICAL_CONTRACT HIS_MEDICAL_CONTRACT { get; set; }
        public virtual HIS_MEDICINE_TYPE HIS_MEDICINE_TYPE { get; set; }
        public virtual HIS_MEDICINE_USE_FORM HIS_MEDICINE_USE_FORM { get; set; }
    }
}