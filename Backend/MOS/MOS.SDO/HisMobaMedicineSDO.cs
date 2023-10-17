
namespace MOS.SDO
{
    public class HisMobaMedicineSDO
    {
        public long MedicineId { get; set; }
        public decimal Amount { get; set; }

        public HisMobaMedicineSDO()
        {
        }

        public HisMobaMedicineSDO(long medicineId, decimal amount)
        {
            this.MedicineId = medicineId;
            this.Amount = amount;
        }
    }
}
