using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisMedicineWithPatySDO
    {
        public HIS_MEDICINE Medicine { get; set; }
        public List<HIS_MEDICINE_PATY> MedicinePaties { get; set; }
        public decimal? Temperature { get; set; }


        public HisMedicineWithPatySDO()
        {
        }

        public HisMedicineWithPatySDO(HIS_MEDICINE medicine, List<HIS_MEDICINE_PATY> medicinePaties)
        {
            this.Medicine = medicine;
            this.MedicinePaties = medicinePaties;
        }
    }
}
