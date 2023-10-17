
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisContactPointViewFilter : FilterBase
    {
        public long? CONTACT_TYPE { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? CONTACT_LEVEL_FROM { get; set; }
        public long? CONTACT_LEVEL_TO { get; set; }
        public string LOGINNAME__EXACT { get; set; }
        public string PATIENT_CODE__EXACT { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }

        public HisContactPointViewFilter()
            : base()
        {
        }
    }
}
