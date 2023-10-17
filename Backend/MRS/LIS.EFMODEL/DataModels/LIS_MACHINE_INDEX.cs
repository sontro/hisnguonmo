//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LIS.EFMODEL.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class LIS_MACHINE_INDEX
    {
        public LIS_MACHINE_INDEX()
        {
            this.LIS_TEST_INDEX_MAP = new HashSet<LIS_TEST_INDEX_MAP>();
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
        public string MACHINE_INDEX_CODE { get; set; }
        public string MACHINE_INDEX_NAME { get; set; }
        public long MACHINE_ID { get; set; }
        public string FORMAT_VALUE { get; set; }
        public Nullable<decimal> RESULT_COEFFICIENT { get; set; }
    
        public virtual LIS_MACHINE LIS_MACHINE { get; set; }
        public virtual ICollection<LIS_TEST_INDEX_MAP> LIS_TEST_INDEX_MAP { get; set; }
    }
}