using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisImpMestMedicineWithInStockAmountSDO : V_HIS_IMP_MEST_MEDICINE
    {
        public decimal? InStockAmount { get; set; }
        public decimal? AvailableAmount { get; set; }
    }
}
