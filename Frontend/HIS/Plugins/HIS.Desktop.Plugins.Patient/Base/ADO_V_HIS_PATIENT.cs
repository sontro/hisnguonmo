using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Patient.Base
{
    public class ADO_V_HIS_PATIENT : V_HIS_PATIENT
    {
        public ADO_V_HIS_PATIENT(){}
        public string DOB_DISPLAY { get; set; }
        public string IS_MALE_DISPLAY1 { get; set; }
        public string SOCIAL_INSURANCE_NUMBER_STR { get; set; }
        public string TDL_HEIN_CARD_NUMBER_STR { get; set; }
        public string MODIFY_TIME_DISPLAY { get; set; }
        public string CREATE_TIME_DISPLAY { get; set; }
    }
}
