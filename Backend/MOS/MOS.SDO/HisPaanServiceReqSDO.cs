using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisPaanServiceReqSDO : HisServiceReqSDO
    {
        public long? PaanPositionId { get; set; }
        public long? PaanLiquidId { get; set; }
        public short? IsMergency { get; set; }
        public long? LiquidTime { get; set; }

        public long PatientTypeId { get; set; }
        public long ServiceId { get; set; }
        public long RoomId { get; set; }
        public long? SereServParentId { get; set; }
        public long? EkipId { get; set; }
        public decimal Amount { get; set; }
        public bool? IsOutParentFee { get; set; }//chi phi ngoai goi dich vu
        public bool? IsExpend { get; set; }

        public long? ExecuteGroupId { get; set; }
        public long InstructionTime { get; set; }
        public long? Priority { get; set; }
        public short? IsNotRequireFee { get; set; }
        public long? TestSampleTypeId { get; set; }
    }
}
