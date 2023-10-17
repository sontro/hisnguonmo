
namespace MOS.SDO
{
    public class HisMaterialPatySDO
    {
        public long PatientTypeId { get; set; }
        public decimal ExpPrice { get; set; }
        public decimal ExpVatRatio { get; set; }

        public HisMaterialPatySDO()
        {
        }

        public HisMaterialPatySDO(long patientTypeId, decimal expPrice, decimal expVatRatio)
        {
            this.PatientTypeId = patientTypeId;
            this.ExpPrice = expPrice;
            this.ExpVatRatio = expVatRatio;
        }
    }
}
