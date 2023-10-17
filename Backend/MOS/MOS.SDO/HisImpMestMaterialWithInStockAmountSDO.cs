using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisImpMestMaterialWithInStockAmountSDO : V_HIS_IMP_MEST_MATERIAL
    {
        public decimal? InStockAmount { get; set; }
        public decimal? AvailableAmount { get; set; }
    }
}
