using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqPatient.ADO
{
    class Printf
    {
        public string PARENT_ORGANIZATION_NAME{get;set;}
        public string TDL_PATIENT_CODE { get; set; }
        public string ORGANIZATION_NAME { get; set; }
        public string TREATMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string PATIENT_NAME { get; set; }
        public string AGE { get; set; }
        public string PATIENT_GENDER_NAME { get; set; }
        public string TDL_HEIN_CARD_NUMBER { get; set; }
        public string HEIN_CARD_TO_TIME { get; set; }
        public string BED_ROOM_NAME { get; set; }
        public string BED_NAME { get; set; }
        public string IN_TIME_STR { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_TEXT { get; set; }
        public string TABLE { get; set; }
    }
}
