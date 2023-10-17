using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisBloodInStockSDO
    {
        public long? Id { get; set; }
        public string NodeId { get; set; }
        public string ParentNodeId { get; set; }
        public long BloodTypeId { get; set; }
        public string BloodTypeCode { get; set; }
        public string BloodTypeName { get; set; }
        public long? MediStockId { get; set; }
        public long? ParentId { get; set; }
        public long? ServiceId { get; set; }
        public decimal? Amount { get; set; }
        public decimal Volume { get; set; }
        public string BloodCode { get; set; }
        public long? ExpiredDate { get; set; }
        public string PackageNumber { get; set; }
        public string SupplierName { get; set; }
        public bool isTypeNode { get; set; }
        public string BloodAboCode { get; set; }
        public string BloodRhCode { get; set; }
    }
}
