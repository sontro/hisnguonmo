using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ExpMaterialTypeSDO
    {
        public long MaterialTypeId { get; set; }
        public long? NumOrder { get; set; }
        public long? ExpMestMatyReqId { get; set; }
        public decimal Amount { get; set; }
        public long? PatientTypeId { get; set; }
        public List<long> ExpMestMaterialIds { get; set; }
        public decimal? Price { get; set; }
        public decimal? VatRatio { get; set; }
        public decimal? DiscountRatio { get; set; }
        public string Description { get; set; }
        public short? IsNotPres { get; set; }
        public long? TreatmentId { get; set; }
        public List<string> DetachKeys { get; set; }
        public string Tutorial { get; set; }
        public long? PriorityMaterialId { get; set; }
        public decimal? PresAmount { get; set; }
    }
}
