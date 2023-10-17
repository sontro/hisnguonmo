using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqExamAdditionSDO
    {
        public long? AdditionServiceId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? PrimaryPatientTypeId { get; set; }
        public long AdditionRoomId { get; set; }
        public long CurrentSereServId { get; set; }
        public long RequestRoomId { get; set; }
        public bool IsNotRequireFee { get; set; }
        public bool IsPrimary { get; set; }//la kham chinh
        public bool IsChangeDepartment { get; set; }//chuyen khoa
        public bool IsFinishCurrent { get; set; } //ket thuc dich vu kham hien tai hay khong
        public long InstructionTime { get; set; }
        public bool IsNotUseBhyt { get; set; }
    }
}
