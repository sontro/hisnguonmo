using System.Collections.Generic;

namespace MOS.SDO
{
    public class PresOutStockMatySDO
    {
        public long? MaterialTypeId { get; set; }
        public long? NumOrder { get; set; }

        public decimal Amount { get; set; }
        public decimal Price { get; set; }

        public string MaterialTypeName { get; set; }
        public string UnitName { get; set; }
        public string Tutorial { get; set; }
        public long? ExpMestReasonId { get; set; }

        public List<long> InstructionTimes { get; set; }
        public decimal? PresAmount { get; set; }
        public string ExceedLimitInPresReason { get; set; }
        public string ExceedLimitInDayReason { get; set; }
    }
}
