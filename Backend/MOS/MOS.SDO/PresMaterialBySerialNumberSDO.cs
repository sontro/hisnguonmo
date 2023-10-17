
using System.Collections.Generic;
namespace MOS.SDO
{
    public class PresMaterialBySerialNumberSDO
    {
        public long MediStockId { get; set; }
        public long? SereServParentId { get; set; }
        public long PatientTypeId { get; set; }
        public long? NumOrder { get; set; }
        public bool IsOutParentFee { get; set; }
        public bool IsExpend { get; set; }
        public bool IsBedExpend { get; set; }
        public long? ExpMestMatyReqId { get; set; }
        public string SerialNumber { get; set; }
        public List<long> InstructionTimes { get; set; }

        public PresMaterialBySerialNumberSDO()
        {
        }

        //Luu y: ko viet ham constructor voi tham so la doi tuong chinh no ==> loi khi dung Mapper
        public PresMaterialBySerialNumberSDO(long mediStockId, long? sereServParentId, long patientTypeId, long? numOrder, bool isExpend, bool isOutParentFee, bool isBedExpend, string serialNumber, long? expMestMatyReqId, List<long> instructionTimes)
        {
            this.MediStockId = mediStockId;
            this.SereServParentId = sereServParentId;
            this.PatientTypeId = patientTypeId;
            this.NumOrder = numOrder;
            this.IsExpend = isExpend;
            this.IsOutParentFee = isOutParentFee;
            this.IsBedExpend = isBedExpend;
            this.SerialNumber = serialNumber;
            this.ExpMestMatyReqId = expMestMatyReqId;
            this.InstructionTimes = instructionTimes;
        }
    }
}
