using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class DHisSereServ2
    {
        public short? IS_NO_EXECUTE { get; set; }
        public long? SERE_SERV_ID { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }
        public long? SERVICE_REQ_ID { get; set; }
        public long? TDL_INTRUCTION_TIME { get; set; }
        public long? TDL_INTRUCTION_DATE { get; set; }
        public long? SERVICE_REQ_STT_ID { get; set; }
        public long? REQUEST_DEPARTMENT_ID { get; set; }
        public long? TDL_SERVICE_TYPE_ID { get; set; }
        public decimal? AMOUNT { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string SERVICE_REQ_CODE { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string REQUEST_DEPARTMENT_NAME { get; set; }
        public short? IS_ANTIBIOTIC_RESISTANCE { get; set; }
        public short? IS_USED { get; set; }
        public long? REQUEST_ROOM_ID { get; set; }
        public string REQUEST_ROOM_NAME { get; set; }
        public string INSTRUCTION_NOTE { get; set; }
        public string TUTORIAL { get; set; }
        public long? TRACKING_ID { get; set; }
        public long? TRACKING_TIME { get; set; }
        public short? IS_RATION { get; set; }
        public string PATIENT_TYPE_NAME { get; set; }
        public long? PRESCRIPTION_TYPE_ID { get; set; }
        public string REQUEST_LOGINNAME { get; set; }
        public string REQUEST_USERNAME { get; set; }
    }
}
