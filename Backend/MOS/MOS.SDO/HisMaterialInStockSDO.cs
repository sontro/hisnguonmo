using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisMaterialInStockSDO
    {
        #region Cac truong tuong ung voi bang cu, de client ko phai sua lai code
        public short? IS_ACTIVE { get; set; }
        public short? MATERIAL_TYPE_IS_ACTIVE { get; set; }
        public long? MEDI_STOCK_ID { get; set; }
        public string SUPPLIER_NAME { get; set; }
        public string SUPPLIER_CODE { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string SERVICE_UNIT_CODE { get; set; }
        public long? NUM_ORDER { get; set; }
        public long? ALERT_EXPIRED_DATE { get; set; }
        public decimal? ALERT_MIN_IN_STOCK { get; set; }
        public short? IS_LEAF { get; set; }
        public long? PARENT_ID { get; set; }
        public long SERVICE_ID { get; set; }
        public string MATERIAL_TYPE_NAME { get; set; }
        public string MATERIAL_TYPE_CODE { get; set; }
        public string REGISTER_NUMBER { get; set; }
        public decimal? EXPIRED_DATE { get; set; }
        public string PACKAGE_NUMBER { get; set; }
        public string BID_NUMBER { get; set; }
        public long? SUPPLIER_ID { get; set; }
        public long? IMP_TIME { get; set; }
        public decimal IMP_VAT_RATIO { get; set; }
        public decimal IMP_PRICE { get; set; }
        public long MATERIAL_TYPE_ID { get; set; }
        public long ID { get; set; }
        public string NATIONAL_NAME { get; set; }
        public string MANUFACTURER_NAME { get; set; }
        public string CONCENTRA { get; set; }
        public string SERIAL_NUMBER { get; set; }
        public string LOCKING_REASON { get; set; }
        #endregion

        public decimal? AvailableAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string NodeId { get; set; }
        public string ParentNodeId { get; set; }
        public bool isTypeNode { get; set; }
        public decimal? BaseAmount { get; set; }
        public decimal? RealBaseAmount { get; set; }
        public decimal? ExpPrice { get; set; }
        public decimal? ExpVatRatio { get; set; }
    }
}
