
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisVitaminAViewFilter : FilterBase
    {
        public string VITAMIN_A_CODE__EXACT { get; set; }
        public string EXP_MEST_CODE__EXACT { get; set; }
        public string TDL_PATIENT_CODE__EXACT { get; set; }

        public long? BRANCH_ID { get; set; }
        public long? MEDICINE_TYPE_ID { get; set; }
        public long? EXP_MEST_ID { get; set; }
        public long? EXP_MEST_TYPE_ID { get; set; }
        public long? EXP_MEST_STT_ID { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public long? PATIENT_ID { get; set; }
        public long? EXECUTE_ROOM_ID { get; set; }
        public long? EXECUTE_DEPARTMENT_ID { get; set; }
        public long? REQUEST_ROOM_ID { get; set; }
        public long? REQUEST_DEPARTMENT_ID { get; set; }


        public List<long> BRANCH_IDs { get; set; }
        public List<long> MEDICINE_TYPE_IDs { get; set; }
        public List<long> EXP_MEST_IDs { get; set; }
        public List<long> EXP_MEST_TYPE_IDs { get; set; }
        public List<long> EXP_MEST_STT_IDs { get; set; }
        public List<long> MEDI_STOCK_IDs { get; set; }
        public List<long> PATIENT_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }
        public List<long> EXECUTE_DEPARTMENT_IDs { get; set; }
        public List<long> REQUEST_ROOM_IDs { get; set; }
        public List<long> REQUEST_DEPARTMENT_IDs { get; set; }
        public VITAMIN_STT_ENUM? VITAMIN_STT { get; set; }

        public long? REQUEST_TIME_FROM { get; set; }
        public long? REQUEST_TIME_TO { get; set; }

        public HisVitaminAViewFilter()
            : base()
        {
        }

        public enum VITAMIN_STT_ENUM
        {
            DRINK = 1,
            NOT_DRINK = 2,
            OUT_OF_STOCK = 3
        }
    }
}
