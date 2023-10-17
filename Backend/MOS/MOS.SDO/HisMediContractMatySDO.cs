using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMediContractMatySDO
    {
        public long? Id { get; set; }
        public long MaterialTypeId { get; set; }
        public long? BidMaterialTypeId { get; set; }
        public long? ManufacturerId { get; set; }
        public decimal Amount { get; set; }
        public decimal? ImpPrice { get; set; }
        public decimal? ImpVatRatio { get; set; }
        public long? ExpiredDate { get; set; }
        public long? ImpExpiredDate { get; set; }
        public string NationalName { get; set; }
        public string Concentra { get; set; }
        public long? HourLifespan { get; set; }
        public long? MonthLifespan { get; set; }
        public long? DayLifespan { get; set; }
        public decimal ContractPrice { get; set; }
        public string BidNumber { get; set; }
        public string BidGroupCode { get; set; }
        public string Note { get; set; }
    }
}
