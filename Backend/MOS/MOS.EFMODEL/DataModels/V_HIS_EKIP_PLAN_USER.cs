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
    
    public partial class V_HIS_EKIP_PLAN_USER
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
        public long EKIP_PLAN_ID { get; set; }
        public string LOGINNAME { get; set; }
        public string USERNAME { get; set; }
        public long EXECUTE_ROLE_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string EXECUTE_ROLE_CODE { get; set; }
        public string EXECUTE_ROLE_NAME { get; set; }
        public Nullable<short> IS_SURG_MAIN { get; set; }
    }
}