using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCard
{
    class AppointmentServiceInfo
    {
        public long TREATMENT_ID { get; set; }
        public long APPOINTMENT_TIME { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_SUB_CODE { get; set; }
        public string ICD_TEXT { get; set; }
        public long? SERVICE_ID { get; set; }
        public decimal? AMOUNT { get; set; }
        public long? PATIENT_TYPE_ID { get; set; }
    }
}
