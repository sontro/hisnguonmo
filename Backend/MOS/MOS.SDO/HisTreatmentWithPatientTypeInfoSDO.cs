using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentWithPatientTypeInfoSDO : HIS_TREATMENT
    {
        public string PREVIOUS_ICD_SUB_CODE { get; set; }
        public string PREVIOUS_ICD_CODE { get; set; }
        public string PREVIOUS_ICD_NAME { get; set; }
        public string PREVIOUS_ICD_TEXT { get; set; }
        public string PREVIOUS_DOCTOR_LOGINNAME { get; set; }
        public string PREVIOUS_DOCTOR_USERNAME { get; set; }
        public string PREVIOUS_END_LOGINNAME { get; set; }
        public string PREVIOUS_END_USERNAME { get; set; }
        public long? PREVIOUS_APPOINTMENT_TIME { get; set; }

        public string PATIENT_TYPE { get; set; }
        public string TREATMENT_TYPE_CODE { get; set; }
        public string PATIENT_TYPE_CODE { get; set; }
        public string HEIN_MEDI_ORG_CODE { get; set; }
        public string RIGHT_ROUTE_TYPE_CODE { get; set; }
        public string HEIN_CARD_NUMBER { get; set; }
        public string LEVEL_CODE { get; set; }
        public string RIGHT_ROUTE_CODE { get; set; }
        public long HEIN_CARD_FROM_TIME { get; set; }
        public long HEIN_CARD_TO_TIME { get; set; }
        public string HEIN_CARD_ADDRESS { get; set; }
        public long SERVER_TIME { get; set; }
        public long PRIMARY_PATIENT_TYPE_ID { get; set; }
    }
}
