using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisMedicalContractSDO
    {
        public long? Id { get; set; }
        public string MedicalContractCode { get; set; }
        public string MedicalContractName { get; set; }
        public long SupplierId { get; set; }
        public long? BidId { get; set; }
        public long? DocumentSupplierId { get; set; }
        public string VentureAgreening { get; set; }
        public string Note { get; set; }
        public long? ValidFromDate { get; set; }
        public long? ValidToDate { get; set; }

        public List<HisMediContractMatySDO> MaterialTypes { get; set; }
        public List<HisMediContractMetySDO> MedicineTypes { get; set; }
    }
}
