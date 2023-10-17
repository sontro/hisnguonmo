using System.Collections.Generic;

namespace MOS.SDO
{
    public class PresOutStockMetySDO
    {
        public long? MedicineTypeId { get; set; }
        public long? NumOrder { get; set; }
        public long? MedicineUseFormId { get; set; }
        public long? UseTimeTo { get; set; }

        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public decimal? Speed { get; set; }

        public string MedicineTypeName { get; set; }
        public string UnitName { get; set; }
        public string Tutorial { get; set; }
        public string Afternoon { get; set; }
        public string Evening { get; set; }
        public string Morning { get; set; }
        public string Noon { get; set; }
        public long? HtuId { get; set; }
        public long? ExpMestReasonId { get; set; }

        public List<long> InstructionTimes { get; set; }
        public decimal? PresAmount { get; set; }
        public long? PreviousUsingCount { get; set; }
        public string ExceedLimitInPresReason { get; set; }
        public string ExceedLimitInDayReason { get; set; }
        public string OddPresReason { get; set; }
        public List<MedicineInfoSDO> MedicineInfoSdos { get; set; }
    }
}
