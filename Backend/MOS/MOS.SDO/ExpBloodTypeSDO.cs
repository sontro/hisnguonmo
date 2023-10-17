using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ExpBloodTypeSDO
    {
        public long? ExpMestBltyReqId { get; set; }
        public long BloodTypeId { get; set; }
        public long? NumOrder { get; set; }
        public long Amount { get; set; }
        public long? BloodAboId { get; set; }
        public long? BloodRhId { get; set; }
        public decimal? Price { get; set; }
        public decimal? VatRatio { get; set; }
        public decimal? DiscountRatio { get; set; }
        public string Description { get; set; }
    }
}
