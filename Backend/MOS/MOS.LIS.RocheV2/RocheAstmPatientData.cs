using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV2
{
    public class RocheAstmPatientData
    {
        private const string IDENTIFIER__PATIENT_RECORD = "P";
        private const string FORMAT = "P|1||{0}||{1}^{2}||{3}|{4}|{5}||{6}|";

        public string PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public RocheAstmGender Gender { get; set; }
        public RocheAstmAddressData Address { get; set; }
        public string HeinCardNumber { get; set; }

        public RocheAstmPatientData()
        {

        }

        public RocheAstmPatientData(string patientId, string firstName, string lastName, DateTime dateOfBirth, RocheAstmGender gender, RocheAstmAddressData address, string heinCardNumber)
        {
            this.PatientId = patientId;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.HeinCardNumber = heinCardNumber;
        }

        public override string ToString()
        {
            return string.Format(FORMAT,
                RocheAstmUtil.NVL(this.PatientId),
                RocheAstmUtil.NVL(this.LastName),
                RocheAstmUtil.NVL(this.FirstName),
                this.DateOfBirthToString(this.DateOfBirth),
                GenderUtil.ToString(this.Gender),
                (this.Address != null ? this.Address.ToString() : ""),
                RocheAstmUtil.NVL(this.HeinCardNumber)
                );
        }

        public static RocheAstmPatientData FromString(string str)
        {
            RocheAstmPatientData patient = null;
            string patientStr = str.StartsWith(RocheAstmPatientData.IDENTIFIER__PATIENT_RECORD) ? str : null;
            if (!string.IsNullOrWhiteSpace(patientStr))
            {
                patient = new RocheAstmPatientData();
                string[] patientContent = patientStr.Split('|');
                if (patientContent != null && patientContent.Length >= 9)
                {
                    patient.PatientId = patientContent[2];

                    //name
                    string name = patientContent[5];
                    string[] nameContent = name.Split('^');
                    if (nameContent != null && nameContent.Length >= 2)
                    {
                        patient.FirstName = nameContent[0];
                        patient.LastName = nameContent[1];
                    }

                    //nam sinh
                    patient.DateOfBirth = DateTime.ParseExact(patientContent[7], "yyyyMMdd", null);

                    //gioi tinh
                    patient.Gender = GenderUtil.ToGender(patientContent[8]);
                }
            }
            return patient;
        }

        private string DateOfBirthToString(DateTime date)
        {
            if (date != null)
            {
                return date.ToString("yyyyMMdd");
            }
            return "";
        }
    }
}
