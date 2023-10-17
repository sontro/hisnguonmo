using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ExpMedicineTypeSDO
    {
        public long MedicineTypeId { get; set; }
        public long? ExpMestMetyReqId { get; set; }
        public long? NumOrder { get; set; }
        public long? NumOfDays { get; set; }
        public long? PatientTypeId { get; set; }
        public List<long> ExpMestMedicineIds { get; set; }
        public long? TreatmentId { get; set; }
        public decimal Amount { get; set; }
        public decimal? Price { get; set; }
        public decimal? VatRatio { get; set; }
        public decimal? DiscountRatio { get; set; }
        public string Description { get; set; }
        public short? IsNotPres { get; set; }
        public long? UseTimeTo { get; set; }

        /// <summary>
        /// Lo thuoc u tien tach
        /// </summary>
        public long? PriorityMedicineId { get; set; }

        public string Tutorial { get; set; }
        public string Afternoon { get; set; }
        public string Evening { get; set; }
        public string Morning { get; set; }
        public string Noon { get; set; }
        public long? HtuId { get; set; }
        public List<string> DetachKeys { get; set; }
        public decimal? PresAmount { get; set; }
    }
}
