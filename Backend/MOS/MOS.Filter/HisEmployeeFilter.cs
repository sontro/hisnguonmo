
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisEmployeeFilter : FilterBase
    {
        public long? ID__NOT_EQUAL { get; set; }
        public long? MEDICINE_TYPE_RANK { get; set; }
        
        public List<string> LOGINNAMEs { get; set; }
        public string LOGINNAME__EXACT { get; set; }

        public HisEmployeeFilter()
            : base()
        {
        }
    }
}
