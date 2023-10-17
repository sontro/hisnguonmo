using System.Collections.Generic;

namespace MOS.SDO
{
    public class PresMedicineSDO
    {
        public long? ServiceConditionId { get; set; }
        public long MediStockId { get; set; }
        public long? SereServParentId { get; set; }
        public string Tutorial { get; set; }
        public string Afternoon { get; set; }
        public string Evening { get; set; }
        public string Morning { get; set; }
        public string Noon { get; set; }
        public string BreathSpeed { get; set; }
        public string BreathTime { get; set; }
        public long? HtuId { get; set; }
        public long? MedicineUseFormId { get; set; }
        public long? NumOfDays { get; set; }
        public long? PreviousUsingCount { get; set; }
        /// <summary>
        /// Cho phep ke theo medicine_type_id hoac medicine_id
        /// </summary>
        public long MedicineTypeId { get; set; }
        public long? MedicineId { get; set; }
        public long PatientTypeId { get; set; }
        public long? NumOrder { get; set; }
        public decimal Amount { get; set; }
        public decimal? Speed { get; set; }
        public bool IsExpend { get; set; }
        public bool IsOutParentFee { get; set; }
        public bool IsBedExpend { get; set; } //hao phi tien giuong
        public List<long> MedicineBeanIds { get; set; }
        /// <summary>
        /// Co su dung don vi goc de ke don hay ko (trong truong hop co khai bao don vi chuyen doi)
        /// </summary>
        public bool UseOriginalUnitForPres { get; set; }
        public List<long> InstructionTimes { get; set; }
        public bool IsNotPres { get; set; }
        public long? OtherPaySourceId { get; set; }
        /// <summary>
        /// Thong tin thuoc pha truyen
        /// </summary>
        public long? MixedInfusion { get; set; }
        public short? IsMixedMain { get; set; }
        public string TutorialInfusion { get; set; }
        public long? ExpMestReasonId { get; set; }
        public decimal? PresAmount { get; set; }
        public string ExceedLimitInPresReason { get; set; }
        public string ExceedLimitInDayReason { get; set; }
        public string OddPresReason { get; set; }
        public List<MedicineInfoSDO> MedicineInfoSdos { get; set; }

        public PresMedicineSDO()
        {
        }

        public PresMedicineSDO(PresMedicineSDO sdo)
        {
            this.MediStockId = sdo.MediStockId;
            this.SereServParentId = sdo.SereServParentId;
            this.Tutorial = sdo.Tutorial;
            this.Morning = sdo.Morning;
            this.Noon = sdo.Noon;
            this.Afternoon = sdo.Afternoon;
            this.Evening = sdo.Evening;
            this.HtuId = sdo.HtuId;
            this.BreathSpeed = sdo.BreathSpeed;
            this.BreathTime = sdo.BreathTime;
            this.PreviousUsingCount = sdo.PreviousUsingCount;
            this.MedicineUseFormId = sdo.MedicineUseFormId;
            this.NumOfDays = sdo.NumOfDays;
            this.MedicineTypeId = sdo.MedicineTypeId;
            this.MedicineId = sdo.MedicineId;
            this.PatientTypeId = sdo.PatientTypeId;
            this.NumOrder = sdo.NumOrder;
            this.Amount = sdo.Amount;
            this.IsExpend = sdo.IsExpend;
            this.IsOutParentFee = sdo.IsOutParentFee;
            this.MedicineBeanIds = sdo.MedicineBeanIds;
            this.Speed = sdo.Speed;
            this.IsBedExpend = sdo.IsBedExpend;
            this.UseOriginalUnitForPres = sdo.UseOriginalUnitForPres;
            this.InstructionTimes = sdo.InstructionTimes;
            this.IsNotPres = sdo.IsNotPres;
            this.ServiceConditionId = sdo.ServiceConditionId;
            this.OtherPaySourceId = sdo.OtherPaySourceId;
            this.MixedInfusion = sdo.MixedInfusion;
            this.IsMixedMain = sdo.IsMixedMain;
            this.TutorialInfusion = sdo.TutorialInfusion;
            this.ExpMestReasonId = sdo.ExpMestReasonId;
            this.PresAmount = sdo.PresAmount;
            this.ExceedLimitInPresReason = sdo.ExceedLimitInPresReason;
            this.ExceedLimitInDayReason = sdo.ExceedLimitInDayReason;
            this.OddPresReason = sdo.OddPresReason;
            this.MedicineInfoSdos = sdo.MedicineInfoSdos;
        }
    }
}
