using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestBloodSDO
    {
        public long Id { get; set; }
        public long BloodTypeId { get; set; }
        public decimal ImpPrice { get; set; }
        public decimal ImpVatRatio { get; set; }
    }
}
