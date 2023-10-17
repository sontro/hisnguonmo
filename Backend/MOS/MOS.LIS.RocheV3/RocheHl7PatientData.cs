using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LIS.RocheV3
{
    public class RocheHl7PatientData
    {
        private const string IDENTIFIER__PATIENT_RECORD = "PID";

        /// <summary>
        /// 0: Ma benh nhan. 2011040178
        /// 1: Ten BN co dau. BÙI THỊ LAN ANH
        /// 2: Ten BN ko dau. BUI THI LAN ANH
        /// 3: Ngay sinh. 19690701
        /// 4: Gioi tinh. F
        /// 5: Dia chi. Thụy Phương, Bắc Từ Liêm, Hà Nội, Việt Nam
        /// 6: SDT. 0919700275
        /// 7: Dien dieu tri. O: Kham; I: Noi tru
        /// 8: Ma dieu tri. 3011230081
        /// </summary>
        private const string FORMAT = "PID|1||{0}||^{1}|^{2}|{3}|{4}|||{5}^^^^||^^^^^^^^^^^{6}"
            + "\r\n"
            + "NTE|1||"
            + "\r\n"
            + "PV1|1|{7}|{8}^^||||||||||||||||^^^^^^^^";

        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public RocheHl7Gender Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsOutPatient { get; set; }
        public string TreatmentCode { get; set; }
         
        public RocheHl7PatientData()
        {
        }

        public RocheHl7PatientData(string patientId, string patientName, DateTime dateOfBirth, RocheHl7Gender gender, string address, string phoneNumber, bool isOutPatient, string treatmentCode)
        {
            this.PatientId = patientId;
            this.PatientName = patientName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
            this.IsOutPatient = IsOutPatient;
            this.TreatmentCode = treatmentCode;
        }

        public override string ToString()
        {
            return string.Format(FORMAT,
                RocheHl7Util.NVL(this.PatientId),
                RocheHl7Util.NVL(this.PatientName),
                Inventec.Common.String.Convert.UnSignVNese2(RocheHl7Util.NVL(this.PatientName)),
                RocheHl7Util.DateOfBirthToString(this.DateOfBirth),
                GenderUtil.ToString(this.Gender),
                RocheHl7Util.NVL(this.Address),
                RocheHl7Util.NVL(this.PhoneNumber),
                this.IsOutPatient ? "O" :"I",
                RocheHl7Util.NVL(this.TreatmentCode)
                );
        }

        public static RocheHl7PatientData FromString(string str)
        {
            return null;
        }
    }
}
