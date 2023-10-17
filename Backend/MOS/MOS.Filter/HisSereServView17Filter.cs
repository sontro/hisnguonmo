using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Filter
{
    public class HisSereServView17Filter: FilterBase
    {
        public List<long> SERVICE_REQ_IDs { get; set; }
        public List<long> SERVICE_TYPE_IDs { get; set; }
        public List<long> NOT_IN_SERVICE_TYPE_IDs { get; set; }
        public List<long> NOT_IN_SERVICE_IDs { get; set; }
        public List<long> EXECUTE_ROOM_IDs { get; set; }

        public long? TDL_TREATMENT_ID { get; set; }
        public long? TREATMENT_ID__NOT_EQUAL { get; set; }
        public long? TDL_PATIENT_ID { get; set; }
        public long? ID__NOT_EQUAL { get; set; }
        public long? PARENT_ID { get; set; }
        public long? SERVICE_REQ_ID { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
        public string HEIN_CARD_NUMBER__EXACT { get; set; }
        public string HEIN_CARD_NUMBER { get; set; }
        public bool? IS_SPECIMEN { get; set; }

        public long? REQUEST_DEPARTMENT_ID { get; set; }
        public long? INTRUCTION_TIME_FROM { get; set; }
        public long? INTRUCTION_TIME_TO { get; set; }
        public long? SERVICE_TYPE_ID { get; set; }
        public long? INVOICE_ID { get; set; }
        public bool? HAS_INVOICE { get; set; }
        public bool? IS_EXPEND { get; set; }
        public bool? HAS_EXECUTE { get; set; }
        public bool? HAS_AMOUNT_TEMP { get; set; }
        public bool? HAS_BED_LOG_ID { get; set; }
        public long? AMOUNT_TEMP { get; set; }

        public HisSereServView17Filter()
            : base()
        {

        }
    }
}
