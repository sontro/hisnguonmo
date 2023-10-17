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
    
    public partial class HIS_CARER_CARD_BORROW
    {
        public HIS_CARER_CARD_BORROW()
        {
            this.HIS_SERVICE_REQ = new HashSet<HIS_SERVICE_REQ>();
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
        public long BORROW_TIME { get; set; }
        public Nullable<long> GIVE_BACK_TIME { get; set; }
        public long CARER_CARD_ID { get; set; }
        public long TREATMENT_ID { get; set; }
        public string GIVING_LOGINNAME { get; set; }
        public string GIVING_USERNAME { get; set; }
        public Nullable<short> IS_LOST { get; set; }
        public string RECEIVING_LOGINNAME { get; set; }
        public string RECEIVING_USERNAME { get; set; }
    
        public virtual HIS_CARER_CARD HIS_CARER_CARD { get; set; }
        public virtual HIS_TREATMENT HIS_TREATMENT { get; set; }
        public virtual ICollection<HIS_SERVICE_REQ> HIS_SERVICE_REQ { get; set; }
    }
}