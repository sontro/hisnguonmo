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
    
    public partial class HIS_CONTACT
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
        public long CONTACT_TIME { get; set; }
        public long CONTACT_POINT1_ID { get; set; }
        public long CONTACT_POINT2_ID { get; set; }
        public string CONTACT_PLACE { get; set; }
    
        public virtual HIS_CONTACT_POINT HIS_CONTACT_POINT { get; set; }
        public virtual HIS_CONTACT_POINT HIS_CONTACT_POINT1 { get; set; }
    }
}