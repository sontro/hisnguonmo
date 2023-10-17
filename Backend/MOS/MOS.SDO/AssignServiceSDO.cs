using System.Collections.Generic;

namespace MOS.SDO
{
    //Doi tuong dung de chi dinh dich vu ky thuat
    public class AssignServiceSDO : HisServiceReqSDO
    {
        //Thong tin cac y/c dich vu chi tiet (la cac dich vu chi tiet can thuc hien vd: xet nghiem men gan, ...)
        public long InstructionTime { get; set; }
        public List<ServiceReqDetailSDO> ServiceReqDetails { get; set; }
        public long? ExecuteGroupId { get; set; }
        public long? Priority { get; set; }
        public long? PriorityTypeId { get; set; }
        public short? IsNotRequireFee { get; set; }
        public bool IsNoExecute { get; set; }
        public bool IsEmergency { get; set; }
        public bool IsInformResultBySms { get; set; }
        public bool ManualRequestRoomId { get; set; }
        public string SessionCode { get; set; }
        public List<long> InstructionTimes { get; set; }
        public List<TrackingInfoSDO> TrackingInfos { get; set; }
        public string Note { get; set; }
    }
}
