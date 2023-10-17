
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisKskDriverViewFilter : FilterBase
    {

        public string KSK_DRIVER_CODE__EXACT { get; set; }
        public string TREATMENT_CODE__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }
        public string SERVICE_REQ_CODE__EXACT { get; set; }


        public long? SERVICE_REQ_ID { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }
        public long? TDL_PATIENT_ID { get; set; }
        public long? EXECUTE_ROOM_ID { get; set; }

        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> SERVICE_REQ_IDs { get; set; }
        public List<long> TDL_TREATMENT_IDs { get; set; }
        public List<long> TDL_PATIENT_IDs { get; set; }

        public long? CONCLUSION_DATE_FROM { get; set; }
        public long? CONCLUSION_DATE_TO { get; set; }

        public long? CONCLUSION_DATE__EQUAL { get; set; }

        public long? SYNC_RESULT_TYPE { get; set; }

        public HisKskDriverViewFilter()
            : base()
        {
        }
    }
}
