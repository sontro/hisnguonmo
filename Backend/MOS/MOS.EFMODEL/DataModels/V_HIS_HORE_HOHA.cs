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
    
    public partial class V_HIS_HORE_HOHA
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
        public long HOLD_RETURN_ID { get; set; }
        public long HORE_HANDOVER_ID { get; set; }
        public long PATIENT_ID { get; set; }
        public string HEIN_CARD_NUMBER { get; set; }
        public long HOLD_TIME { get; set; }
        public string HOLD_LOGINNAME { get; set; }
        public string HOLD_USERNAME { get; set; }
        public Nullable<long> RETURN_TIME { get; set; }
        public string RETURN_LOGINNAME { get; set; }
        public string RETURN_USERNAME { get; set; }
        public string PATIENT_CODE { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string VIR_PATIENT_NAME { get; set; }
        public long GENDER_ID { get; set; }
        public long DOB { get; set; }
        public Nullable<short> IS_HAS_NOT_DAY_DOB { get; set; }
        public string VIR_ADDRESS { get; set; }
        public string GENDER_CODE { get; set; }
        public string GENDER_NAME { get; set; }
        public string DOC_HOLD_TYPE_NAMES { get; set; }
    }
}