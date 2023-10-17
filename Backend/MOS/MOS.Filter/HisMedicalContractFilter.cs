
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicalContractFilter : FilterBase
    {
        public string MEDICAL_CONTRACT_CODE__EXACT { get; set; }
        public List<string> MEDICAL_CONTRACT_CODEs { get; set; }

        public long? SUPPLIER_ID { get; set; }
        public long? DOCUMENT_SUPPLIER_ID { get; set; }
        public long? BID_ID { get; set; }

        public List<long> SUPPLIER_IDs { get; set; }
        public List<long> DOCUMENT_SUPPLIER_IDs { get; set; }
        public List<long> BID_IDs { get; set; }

        public long? CREATE_DATE_FROM { get; set; }
        public long? CREATE_DATE_TO { get; set; }

        public HisMedicalContractFilter()
            : base()
        {
        }
    }
}
