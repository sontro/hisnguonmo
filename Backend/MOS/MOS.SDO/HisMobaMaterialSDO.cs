
namespace MOS.SDO
{
    public class HisMobaMaterialSDO
    {
        public long MaterialId { get; set; }
        public decimal Amount { get; set; }
        
        public HisMobaMaterialSDO()
        {
        }

        public HisMobaMaterialSDO(long materialId, decimal amount)
        {
            this.MaterialId = materialId;
            this.Amount = amount;
        }
    }
}
