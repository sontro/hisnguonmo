using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqSDO
    {
        public long? NumOrder { get; set; }
        public long? Id { get; set; }
        public long? ParentServiceReqId { get; set; }
        public string IcdText { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string IcdCauseCode { get; set; }
        public string IcdCauseName { get; set; }
        public string IcdSubCode { get; set; }
        public string ProvisionalDiagnosis { get; set; }
        public string Description { get; set; }
        public long TreatmentId { get; set; }
        public long? TrackingId { get; set; }
        public long RequestRoomId { get; set; }
        public string RequestLoginName { get; set; }
        public string RequestUserName { get; set; }
        public string ConsultantLoginName { get; set; }
        public string ConsultantUserName { get; set; }
        public long? KidneyShift { get; set; }
        public long? MachineId { get; set; }
        public long? ExpMestTemplateId { get; set; }
        public bool IsKidney { get; set; }
    }
}
