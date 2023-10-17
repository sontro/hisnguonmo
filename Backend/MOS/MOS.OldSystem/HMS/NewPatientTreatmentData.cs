using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OldSystem.HMS
{
    public class NewPatientTreatmentData
    {
        public string PatientCode { get; set; }
        public string TreatmentCode { get; set; }
        public string PatientName { get; set; }
        /// <summary>
        /// dd/MM/yyyy hh:mm:ss
        /// </summary>
        public string DateOfBirth { get; set; }
        public bool IsMale { get; set; }
        public string Address { get; set; }
        public int CommuneCode { get; set; }
        public int DistrictCode { get; set; }
        public int ProvinceCode { get; set; }
        public string RelativeInfo { get; set; }
        public string ExamRoomCode { get; set; }
        public int ExamStyleId { get; set; }
        public bool IsEmergency { get; set; }
        public int CareerCode { get; set; }
        public bool IsRightRoute { get; set; }
        public string TransferHeinOrgCode { get; set; }
        public string TransferIcd { get; set; }
        public string Creator { get; set; }
        public int EthnicCode { get; set; }
    }
}
