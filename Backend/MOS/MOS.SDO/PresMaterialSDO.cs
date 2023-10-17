
using System.Collections.Generic;
namespace MOS.SDO
{
    public class PresMaterialSDO
    {
        public long MediStockId { get; set; }
        public decimal Amount { get; set; }
        public decimal? FailedAmount { get; set; }
        public long? SereServParentId { get; set; }
        public long MaterialTypeId { get; set; }
        public long? MaterialId { get; set; }
        public long PatientTypeId { get; set; }
        public long? NumOrder { get; set; }
        public string Tutorial { get; set; }
        public bool IsOutParentFee { get; set; }
        public bool IsExpend { get; set; }
        public bool IsBedExpend { get; set; }
        public long? EquipmentSetId { get; set; }
        public List<long> InstructionTimes { get; set; }
        public bool IsNotPres { get; set; }
        public List<long> MaterialBeanIds { get; set; }
        public long? ServiceConditionId { get; set; }
        public long? OtherPaySourceId { get; set; }
        public long? ExpMestReasonId { get; set; }
        public decimal? PresAmount { get; set; }
        public string ExceedLimitInPresReason { get; set; }
        public string ExceedLimitInDayReason { get; set; }

        public PresMaterialSDO()
        {
        }

        //Luu y: ko viet ham constructor voi tham so la doi tuong chinh no ==> loi khi dung Mapper
        public PresMaterialSDO(long mediStockId, long? sereServParentId, long materialTypeId, long patientTypeId, long? numOrder, decimal amount, bool isExpend, bool isOutParentFee, List<long> materialBeanIds, long? equipmentSetId, bool isBedExpend, long? materialId, List<long> instructionTimes, decimal? failedAmount, string tutorial, bool isNotPres, long? serviceConditionId, long? otherPaySourceId)
        {
            this.MediStockId = mediStockId;
            this.SereServParentId = sereServParentId;
            this.MaterialTypeId = materialTypeId;
            this.MaterialId = materialId;
            this.PatientTypeId = patientTypeId;
            this.NumOrder = numOrder;
            this.Amount = amount;
            this.IsExpend = isExpend;
            this.IsOutParentFee = isOutParentFee;
            this.MaterialBeanIds = materialBeanIds;
            this.EquipmentSetId = equipmentSetId;
            this.IsBedExpend = isBedExpend;
            this.InstructionTimes = instructionTimes;
            this.FailedAmount = failedAmount;
            this.Tutorial = tutorial;
            this.IsNotPres = IsNotPres;
            this.ServiceConditionId = serviceConditionId;
            this.OtherPaySourceId = otherPaySourceId;
        }

        //Luu y: ko viet ham constructor voi tham so la doi tuong chinh no ==> loi khi dung Mapper
        public PresMaterialSDO(long mediStockId, long? sereServParentId, long materialTypeId, long patientTypeId, long? numOrder, decimal amount, bool isExpend, bool isOutParentFee, List<long> materialBeanIds, long? equipmentSetId, bool isBedExpend, long? materialId, List<long> instructionTimes, decimal? failedAmount, string tutorial, bool isNotPres, long? serviceConditionId, long? otherPaySourceId, decimal? presAmount)
        {
            this.MediStockId = mediStockId;
            this.SereServParentId = sereServParentId;
            this.MaterialTypeId = materialTypeId;
            this.MaterialId = materialId;
            this.PatientTypeId = patientTypeId;
            this.NumOrder = numOrder;
            this.Amount = amount;
            this.IsExpend = isExpend;
            this.IsOutParentFee = isOutParentFee;
            this.MaterialBeanIds = materialBeanIds;
            this.EquipmentSetId = equipmentSetId;
            this.IsBedExpend = isBedExpend;
            this.InstructionTimes = instructionTimes;
            this.FailedAmount = failedAmount;
            this.Tutorial = tutorial;
            this.IsNotPres = IsNotPres;
            this.ServiceConditionId = serviceConditionId;
            this.OtherPaySourceId = otherPaySourceId;
            this.PresAmount = presAmount;
        }

        public PresMaterialSDO(long mediStockId, long? sereServParentId, long materialTypeId, long patientTypeId, long? numOrder, decimal amount, bool isExpend, bool isOutParentFee, List<long> materialBeanIds, long? equipmentSetId, bool isBedExpend, long? materialId, List<long> instructionTimes, decimal? failedAmount, string tutorial, bool isNotPres, long? serviceConditionId, long? otherPaySourceId, decimal? presAmount, string exceedLimitInPresReason, string exceedLimitInDayReason)
        {
            this.MediStockId = mediStockId;
            this.SereServParentId = sereServParentId;
            this.MaterialTypeId = materialTypeId;
            this.MaterialId = materialId;
            this.PatientTypeId = patientTypeId;
            this.NumOrder = numOrder;
            this.Amount = amount;
            this.IsExpend = isExpend;
            this.IsOutParentFee = isOutParentFee;
            this.MaterialBeanIds = materialBeanIds;
            this.EquipmentSetId = equipmentSetId;
            this.IsBedExpend = isBedExpend;
            this.InstructionTimes = instructionTimes;
            this.FailedAmount = failedAmount;
            this.Tutorial = tutorial;
            this.IsNotPres = IsNotPres;
            this.ServiceConditionId = serviceConditionId;
            this.OtherPaySourceId = otherPaySourceId;
            this.PresAmount = presAmount;
            this.ExceedLimitInPresReason = exceedLimitInPresReason;
            this.ExceedLimitInDayReason = exceedLimitInDayReason;
        }

        //Luu y: ko viet ham constructor voi tham so la doi tuong chinh no ==> loi khi dung Mapper
        public PresMaterialSDO(long mediStockId, long? sereServParentId, long materialTypeId, long patientTypeId, long? numOrder, decimal amount, bool isExpend, bool isOutParentFee, List<long> materialBeanIds, long? equipmentSetId, bool isBedExpend, long? materialId, List<long> instructionTimes, decimal? failedAmount, string tutorial, bool isNotPres, long? serviceConditionId, long? otherPaySourceId, long? expMestReasonId)
        {
            this.MediStockId = mediStockId;
            this.SereServParentId = sereServParentId;
            this.MaterialTypeId = materialTypeId;
            this.MaterialId = materialId;
            this.PatientTypeId = patientTypeId;
            this.NumOrder = numOrder;
            this.Amount = amount;
            this.IsExpend = isExpend;
            this.IsOutParentFee = isOutParentFee;
            this.MaterialBeanIds = materialBeanIds;
            this.EquipmentSetId = equipmentSetId;
            this.IsBedExpend = isBedExpend;
            this.InstructionTimes = instructionTimes;
            this.FailedAmount = failedAmount;
            this.Tutorial = tutorial;
            this.IsNotPres = IsNotPres;
            this.ServiceConditionId = serviceConditionId;
            this.OtherPaySourceId = otherPaySourceId;
            this.ExpMestReasonId = expMestReasonId;
        }

        public PresMaterialSDO(long mediStockId, long? sereServParentId, long materialTypeId, long patientTypeId, long? numOrder, decimal amount, bool isExpend, bool isOutParentFee, List<long> materialBeanIds, long? equipmentSetId, bool isBedExpend, long? materialId, List<long> instructionTimes, decimal? failedAmount, string tutorial, bool isNotPres, long? serviceConditionId, long? otherPaySourceId, long? expMestReasonId, decimal? presAmount)
        {
            this.MediStockId = mediStockId;
            this.SereServParentId = sereServParentId;
            this.MaterialTypeId = materialTypeId;
            this.MaterialId = materialId;
            this.PatientTypeId = patientTypeId;
            this.NumOrder = numOrder;
            this.Amount = amount;
            this.IsExpend = isExpend;
            this.IsOutParentFee = isOutParentFee;
            this.MaterialBeanIds = materialBeanIds;
            this.EquipmentSetId = equipmentSetId;
            this.IsBedExpend = isBedExpend;
            this.InstructionTimes = instructionTimes;
            this.FailedAmount = failedAmount;
            this.Tutorial = tutorial;
            this.IsNotPres = IsNotPres;
            this.ServiceConditionId = serviceConditionId;
            this.OtherPaySourceId = otherPaySourceId;
            this.ExpMestReasonId = expMestReasonId;
            this.PresAmount = presAmount;
        }
    }
}
