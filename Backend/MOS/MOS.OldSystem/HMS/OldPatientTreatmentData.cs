using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OldSystem.HMS
{
    public class OldPatientTreatmentData
    {
        public string PatientCode { get; set; }
        public string TreatmentCode { get; set; }
        public string ExamRoomCode { get; set; }
        public int ExamStyleId { get; set; }
        public bool IsEmergency { get; set; }
        public bool IsRightRoute { get; set; }
        public string CardNumber { get; set; }
        public string HeinOrgCode { get; set; }
        /// <summary>
        /// dd/MM/yyyy hh:mm:ss
        /// </summary>
        public string CardFromDate { get; set; }
        /// <summary>
        /// dd/MM/yyyy hh:mm:ss
        /// </summary>
        public string CardToDate { get; set; }
        public string TransferHeinOrgCode { get; set; }
        public string TransferIcdCode { get; set; }
        public string Creator { get; set; }
    }
}
