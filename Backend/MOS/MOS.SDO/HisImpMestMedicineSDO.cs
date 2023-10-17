using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestMedicineSDO
    {
        public long Id { get; set; }
        public long MedicineTypeId { get; set; }
        public decimal Amount { get; set; }
        public decimal ImpPrice { get; set; }
        public long? DocumentPrice { get; set; }
        public decimal ImpVatRatio { get; set; }
        public string PackageNumber { get; set; }
        public long? ExpireDate { get; set; }
        public decimal? Temperature { get; set; }
    }
}
