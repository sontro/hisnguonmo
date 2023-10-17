using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisBedServiceSDO
    {
        //Thong tin cac y/c dich vu chi tiet (la cac dich vu chi tiet can thuc hien vd: xet nghiem men gan, ...)
        public List<ServiceReqDetailSDO> ServiceReqDetails { get; set; }
        public long? ParentId { get; set; }
        public long InstructionTime { get; set; }
        public long? Priority { get; set; }
        public long BedLogId { get; set; }
        public short? IsNotRequireFee { get; set; }
        public string Description { get; set; }
    }

    public class HisBedServiceReqSDO
    {
        public long TreatmentId { get; set; }
        public long RequestRoomId { get; set; }
        public List<HisBedServiceSDO> BedServices { get; set; }
    }
}
