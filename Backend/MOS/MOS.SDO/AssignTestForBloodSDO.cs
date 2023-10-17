using System.Collections.Generic;

namespace MOS.SDO
{
    public class AssignTestForBloodSDO
    {
        public long InstructionTime { get; set; }
        public long ServiceReqId { get; set; }//id cua phieu xuat mau
        public List<ServiceReqDetailSDO> ServiceReqDetails { get; set; }
    }
}
