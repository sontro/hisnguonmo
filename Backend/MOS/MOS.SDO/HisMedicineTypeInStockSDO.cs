using System;

namespace MOS.SDO
{
    public class HisMedicineTypeInStockSDO
    {
        public long Id { get; set; }
        public string MedicineTypeCode { get; set; }
        public string MedicineTypeName { get; set; }
        public long ServiceId { get; set; }
        public Nullable<long> ParentId { get; set; }
        public Nullable<long> MediStockId { get; set; }
        public Nullable<long> ServiceUnitId { get; set; }
        public Nullable<short> IsLeaf { get; set; }
        public Nullable<short> IsActive { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> AvailableAmount { get; set; }
        public string MediStockCode { get; set; }
        public string MediStockName { get; set; }
        public string ServiceUnitCode { get; set; }
        public string ServiceUnitName { get; set; }
        public string ServiceUnitSymbol { get; set; }
        public string NationalCode { get; set; }
        public string NationalName { get; set; }
        public string ManufacturerCode { get; set; }
        public string ManufacturerName { get; set; }
        public string ActiveIngrBhytName { get; set; }
        public string ActiveIngrBhytCode { get; set; }
        public string RegisterNumber { get; set; }
        public string Concentra { get; set; }
        public Nullable<long> NumOrder { get; set; }
        public Nullable<short> IsGoodsRestrict { get; set; }
        public Nullable<short> IsBusiness { get; set; }
        public Nullable<decimal> ImpStockTotalAmount { get; set; }
        public Nullable<decimal> ImpStockAvailableAmount { get; set; }
        public Nullable<decimal> ExportedTotalAmount { get; set; }
        public Nullable<decimal> BaseAmount { get; set; }
        public Nullable<decimal> LastExpPrice { get; set; }
        public Nullable<decimal> LastExpVatRatio { get; set; }
        public Nullable<long> LastExpiredDate { get; set; }
    }
}
