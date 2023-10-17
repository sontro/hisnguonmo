using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class RationServiceSDO
    {
        public long PatientTypeId { get; set; }
        public long RoomId { get; set; }
        public decimal Amount { get; set; }
        public long ServiceId { get; set; }
        public string InstructionNote { get; set; }
        public List<long> RationTimeIds { get; set; }
        public long? SereServRationId { get; set; } //  HIS_SERE_SERV_RATION cần sửa thông tin.
    }

    public class HisRationServiceReqSDO
    {
        public List<long> TreatmentIds { get; set; }
        public List<long> InstructionTimes { get; set; }
        public List<RationServiceSDO> RationServices { get; set; }
        public long? ParentServiceReqId { get; set; }
        public string IcdText { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string IcdCauseCode { get; set; }
        public string IcdCauseName { get; set; }
        public string IcdSubCode { get; set; }
        public string Description { get; set; }
        public bool HalfInFirstDay { get; set; }
        public long? TrackingId { get; set; }
        public long RequestRoomId { get; set; }
        public string RequestLoginName { get; set; }
        public string RequestUserName { get; set; }
        public bool IsForAutoCreateRation { get; set; }
        public bool IsForHomie { get; set; }
    }
}
