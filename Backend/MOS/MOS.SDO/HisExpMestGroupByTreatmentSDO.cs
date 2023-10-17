using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestGroupByTreatmentSDO
    {
        public long TREATMENT_ID { get; set; }
        public string TREATMENT_CODE { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public long TDL_PATIENT_DOB { get; set; }
        public short? TDL_PATIENT_IS_HAS_NOT_DAY_DOB { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public long? OUT_TIME { get; set; }
        public string EXP_MEST_IDS { get; set; }
    }
}
