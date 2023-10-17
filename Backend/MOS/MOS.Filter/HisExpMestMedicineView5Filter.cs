using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestMedicineView5Filter : FilterBase
    {
        public List<long> EXP_MEST_IDs { get; set; }
        public List<long> MEDICINE_IDs { get; set; }
        public List<long> TDL_MEDICINE_TYPE_IDs { get; set; }
        public List<long> TDL_SERVICE_REQ_IDs { get; set; }
        public List<long> TDL_TREATMENT_IDs { get; set; }
        public List<long> VACCINATION_RESULT_IDs { get; set; }
        public List<long> TDL_VACCINATION_IDs { get; set; }
        public List<long> PATIENT_TYPE_IDs { get; set; }
        public List<long> PATIENT_IDs { get; set; }

        public long? VACCINATION_RESULT_ID { get; set; }
        public long? TDL_VACCINATION_ID { get; set; }
        public long? EXP_MEST_ID { get; set; }
        public long? TDL_MEDICINE_TYPE_ID { get; set; }
        public long? MEDICINE_ID { get; set; }
        public long? TDL_SERVICE_REQ_ID { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public long? PATIENT_ID { get; set; }

        public long? EXP_TIME_FROM { get; set; }
        public long? EXP_TIME_TO { get; set; }

        public HisExpMestMedicineView5Filter()
            : base()
        {
        }
    }
}
