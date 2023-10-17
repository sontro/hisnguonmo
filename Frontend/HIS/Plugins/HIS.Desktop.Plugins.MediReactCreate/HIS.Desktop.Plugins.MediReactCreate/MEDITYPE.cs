using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediReactCreate
{
    class MEDITYPE
    {
        public string ID { get; set; }
        public long? INSTRUCTION_TIME { get; set; }
        public string INSTRUCTION_TIME_STR { get; set; }
        public string INSTRUCTION_DATE_STR { get; set; }
        public long? MEDICINE_ID { get; set; }
        public string MEDICINE_TYPE_CODE { get; set; }
        public string MEDICINE_TYPE_NAME { get; set; }
        public string PACKAGE_NUMBER { get; set; }
        public long? SERVICE_UNIT_ID { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public long? EXPIRED_DATE { get; set; }
        public string EXPIRED_DATE_STR { get; set; }
        public decimal? SPEED { get; set; }
        public string LOGGINNAME { get; set; }
        public Boolean ngoaikho { get; set; }
        public string mediType { get; set; }
    }
}
