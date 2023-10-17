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
    
    public partial class HIS_OBEY_CONTRAINDI
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
        public string REQUEST_LOGINNAME { get; set; }
        public Nullable<long> REQUEST_DEPARTMENT_ID { get; set; }
        public long TREATMENT_ID { get; set; }
        public Nullable<long> INTRUCTION_TIME { get; set; }
        public Nullable<decimal> AMOUNT { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_SUB_CODE { get; set; }
        public string ICD_TEXT { get; set; }
        public Nullable<long> SERVICE_ID { get; set; }
        public Nullable<decimal> REQUEST_ROOM_ID { get; set; }
        public string SERVICE_REQ_CODES { get; set; }
        public string EXP_MEST_CODES { get; set; }
    
        public virtual HIS_DEPARTMENT HIS_DEPARTMENT { get; set; }
        public virtual HIS_TREATMENT HIS_TREATMENT { get; set; }
    }
}