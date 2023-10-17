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
    
    public partial class HIS_TEST_INDEX
    {
        public HIS_TEST_INDEX()
        {
            this.HIS_SERE_SERV_TEIN = new HashSet<HIS_SERE_SERV_TEIN>();
            this.HIS_TEST_INDEX_RANGE = new HashSet<HIS_TEST_INDEX_RANGE>();
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
        public string TEST_INDEX_CODE { get; set; }
        public string TEST_INDEX_NAME { get; set; }
        public long TEST_SERVICE_TYPE_ID { get; set; }
        public Nullable<long> TEST_INDEX_UNIT_ID { get; set; }
        public Nullable<long> NUM_ORDER { get; set; }
        public string DEFAULT_VALUE { get; set; }
        public string BHYT_CODE { get; set; }
        public string BHYT_NAME { get; set; }
        public Nullable<short> IS_NOT_SHOW_SERVICE { get; set; }
        public Nullable<short> IS_IMPORTANT { get; set; }
        public Nullable<long> TEST_INDEX_GROUP_ID { get; set; }
        public Nullable<short> IS_TO_CALCULATE_EGFR { get; set; }
        public Nullable<decimal> NORMATION_AMOUNT { get; set; }
        public Nullable<long> MATERIAL_TYPE_ID { get; set; }
        public Nullable<short> IS_BLOOD_ABO { get; set; }
        public Nullable<short> IS_BLOOD_RH { get; set; }
        public Nullable<short> IS_HBSAG { get; set; }
        public Nullable<short> IS_HCV { get; set; }
        public Nullable<short> IS_HIV { get; set; }
        public Nullable<short> IS_TEST_HARMONY_BLOOD { get; set; }
        public Nullable<decimal> CONVERT_RATIO_MLCT { get; set; }
        public string RESULT_BLOOD_A { get; set; }
        public string RESULT_BLOOD_B { get; set; }
        public string RESULT_BLOOD_AB { get; set; }
        public string RESULT_BLOOD_O { get; set; }
        public string RESULT_BLOOD_RH_PLUS { get; set; }
        public string RESULT_BLOOD_RH_MINUS { get; set; }
    
        public virtual HIS_MATERIAL_TYPE HIS_MATERIAL_TYPE { get; set; }
        public virtual ICollection<HIS_SERE_SERV_TEIN> HIS_SERE_SERV_TEIN { get; set; }
        public virtual HIS_SERVICE HIS_SERVICE { get; set; }
        public virtual HIS_TEST_INDEX_UNIT HIS_TEST_INDEX_UNIT { get; set; }
        public virtual HIS_TEST_INDEX_GROUP HIS_TEST_INDEX_GROUP { get; set; }
        public virtual ICollection<HIS_TEST_INDEX_RANGE> HIS_TEST_INDEX_RANGE { get; set; }
    }
}
