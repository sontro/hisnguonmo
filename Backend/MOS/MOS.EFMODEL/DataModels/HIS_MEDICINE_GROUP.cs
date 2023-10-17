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
    
    public partial class HIS_MEDICINE_GROUP
    {
        public HIS_MEDICINE_GROUP()
        {
            this.HIS_MEDICINE_TYPE = new HashSet<HIS_MEDICINE_TYPE>();
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
        public string MEDICINE_GROUP_CODE { get; set; }
        public string MEDICINE_GROUP_NAME { get; set; }
        public Nullable<long> NUM_ORDER { get; set; }
        public Nullable<short> IS_SEPARATE_PRINTING { get; set; }
        public Nullable<short> IS_NUMBERED_TRACKING { get; set; }
        public Nullable<short> IS_WARNING { get; set; }
        public Nullable<long> NUMBER_DAY { get; set; }
        public Nullable<short> IS_AUTO_TREATMENT_DAY_COUNT { get; set; }
    
        public virtual ICollection<HIS_MEDICINE_TYPE> HIS_MEDICINE_TYPE { get; set; }
    }
}