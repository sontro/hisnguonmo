
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisTransfusionSumViewFilter : FilterBase
    {
        public string TREATMENT_CODE__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }
        public string BLOOD_TYPE_CODE__EXACT { get; set; }
        public string BLOOD_CODE__EXACT { get; set; }

        public long? TREATMENT_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? ROOM_ID { get; set; }
        public long? EXP_MEST_BLOOD_ID { get; set; }
        public long? BLOOD_TYPE_ID { get; set; }
        public long? BLOOD_ABO_ID { get; set; }
        public long? BLOOD_RH_ID { get; set; }
        public long? BLOOD_VOLUME_ID { get; set; }


        public List<long> TREATMENT_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<long> ROOM_IDs { get; set; }
        public List<long> EXP_MEST_BLOOD_IDs { get; set; }
        public List<long> BLOOD_TYPE_IDs { get; set; }
        public List<long> BLOOD_ABO_IDs { get; set; }
        public List<long> BLOOD_RH_IDs { get; set; }
        public List<long> BLOOD_VOLUME_IDs { get; set; }

        public HisTransfusionSumViewFilter()
            : base()
        {
        }
    }
}
