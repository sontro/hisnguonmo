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
    
    public partial class HIS_MEDI_RECORD_TYPE
    {
        public HIS_MEDI_RECORD_TYPE()
        {
            this.HIS_MEDI_RECORD = new HashSet<HIS_MEDI_RECORD>();
            this.HIS_TREATMENT = new HashSet<HIS_TREATMENT>();
        }
    
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
        public string MEDI_RECORD_TYPE_CODE { get; set; }
        public string MEDI_RECORD_TYPE_NAME { get; set; }
    
        public virtual ICollection<HIS_MEDI_RECORD> HIS_MEDI_RECORD { get; set; }
        public virtual ICollection<HIS_TREATMENT> HIS_TREATMENT { get; set; }
    }
}