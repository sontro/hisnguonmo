using System;

namespace MOS.SDO
{
    public class HisBloodTypeInStockSDO
    {
        public long Id { get; set; }
        public string BloodTypeCode { get; set; }
        public string BloodTypeName { get; set; }
        public string BloodTypeHeinName { get; set; }
        public long ServiceId { get; set; }
        public Nullable<long> ParentId { get; set; }
        public Nullable<long> MediStockId { get; set; }
        public Nullable<short> IsLeaf { get; set; }
        public Nullable<short> IsActive { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public decimal Volume { get; set; }
        public Nullable<long> NumOrder { get; set; }
    }
}
