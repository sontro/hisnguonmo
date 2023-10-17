
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisAreaFilter : FilterBase
    {
        public string AREA_CODE__EXACT { get; set; }

        public long? DEPARTMENT_ID { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }

        public HisAreaFilter()
            : base()
        {
        }
    }
}
