using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Register.ADO
{
    public class PatientInformationADO
    {
        public long? BLOOD_ABO_ID { get; set; }
        public string BLOOD_ABO_CODE { get; set; }
        public long? BLOOD_RH_ID { get; set; }
        public string BLOOD_RH_CODE { get; set; }
        public long? CAREER_ID { get; set; }
        public string CAREER_NAME { get; set; }
        public string HOUSEHOLD_RELATION_NAME { get; set; }
        public long? HOUSEHOLD_RELATION_ID { get; set; }
        public string HOUSEHOLD_CODE { get; set; }
        public string BORN_PROVINCE_CODE { get; set; }
        public string BORN_PROVINCE_NAME { get; set; }
        public string MOTHER_NAME { get; set; }
        public string FATHER_NAME { get; set; }
        public string RELATIVE_ADDRESS { get; set; } 
        public string RELATIVE_NAME { get; set; }
        public string RELATIVE_TYPE { get; set; }
        public string RELATIVE_MOBILE { get; set; }
        public string RELATIVE_CMND_NUMBER { get; set; }
        public string HT_PROVINCE_NAME { get; set; }
        public string HT_PROVINCE_CODE { get; set; }
        public string HT_DISTRICT_NAME { get; set; }
        public string HT_DISTRICT_CODE { get; set; }
        public string HT_COMMUNE_NAME { get; set; }
        public string HT_COMMUNE_CODE { get; set; }
        public string HT_ADDRESS { get; set; }
        public string CMND_NUMBER { get; set; }
        public long? CMND_DATE { get; set; }
        public string CMND_PLACE { get; set; }
        public string MOBILE { get; set; }
        public string EMAIL { get; set; }
        public string ETHNIC_NAME { get; set; }
        public string NATIONAL_NAME { get; set; }
        public bool IsPatientOld { get; set; }
        public bool IsEdited { get; set; }
    }
}
