using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisImpMestBloodWithInStockInfoSDO : V_HIS_IMP_MEST_BLOOD
    {
        public bool IsInStock { get; set; }
        public bool IsAvailable { get; set; }
    }
}
