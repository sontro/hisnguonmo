using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OldSystem.HMS
{
    public class ServiceReqData
    {
        public string CreateTime { get; set; }
        public string PatientCode { get; set; }
        public string TreatmentCode { get; set; }
        public string ServiceReqCode { get; set; }
        public string ReqRoomCode { get; set; }
        public string GroupCode { get; set; }
        public List<string> SubclinicalCodes { get; set; }
        public bool IsEmergency { get; set; }
        public bool IsUseBhyt { get; set; }
        public bool IsUseService { get; set; }
        public string IcdName { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public string RequestLoginname { get; set; }
    }
}
