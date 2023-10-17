
namespace MOS.SDO
{
    public class HisMedicinePatySDO
    {
        public long PatientTypeId { get; set; }
        public decimal ExpPrice { get; set; }
        public decimal ExpVatRatio { get; set; }

        public HisMedicinePatySDO()
        {
        }

        public HisMedicinePatySDO(long patientTypeId, decimal expPrice, decimal expVatRatio)
        {
            this.PatientTypeId = patientTypeId;
            this.ExpPrice = expPrice;
            this.ExpVatRatio = expVatRatio;
        }
    }
}
