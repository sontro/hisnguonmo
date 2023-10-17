using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class PatientData
    {
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public long Dob { get; set; }
        public long GenderId { get; set; }
        public bool IsHasNotDayDob { get; set; }

        public PatientData()
        {
        }

        public PatientData(string patientCode, string patientName, long dob, long genderId, bool isHasNotDayDob)
        {
            this.PatientCode = patientCode;
            this.PatientName = patientName;
            this.Dob = dob;
            this.GenderId = genderId;
            this.IsHasNotDayDob = isHasNotDayDob;
        }

        public override string ToString()
        {
            HIS_GENDER gender = HisGenderCFG.DATA.Where(o => o.ID == this.GenderId).FirstOrDefault();
            string patientCode = PatientCode != null ? PatientCode : "";
            string patientName = PatientName != null ? PatientName : "";
            string genderName = gender != null ? gender.GENDER_NAME : "";
            string dob = "";
            try
            {
                dob = this.IsHasNotDayDob ? this.Dob.ToString().Substring(0, 4) : Inventec.Common.DateTime.Convert.TimeNumberToDateString(this.Dob);
            }
            catch(Exception ex)
            {
                LogSystem.Error(ex);
                dob = "";
            }

            return string.Format("{0}: {1} ({2} - {3} - {4})", SimpleEventKey.PATIENT_CODE, patientCode, patientName, dob, genderName);
        }
    }
}
