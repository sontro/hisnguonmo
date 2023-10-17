using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentStoreSDO
    {
        public List<long> TreatmentIds { get; set; }
        public bool? IsOutPatient { get; set; }
        public long StoreTime { get; set; }
        public long DataStoreId { get; set; }
        public long? ProgramId { get; set; }
        public long? MediRecordTypeId { get; set; }
        public string StoreCode { get; set; }
        public bool? IsUseEndCode { get; set; }
        public long? LocationStoreId { get; set; }
    }
}
