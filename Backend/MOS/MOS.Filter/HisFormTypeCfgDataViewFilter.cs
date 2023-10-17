
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisFormTypeCfgDataViewFilter : FilterBase
    {
        public string FORM_TYPE_CODE__EXACT { get; set; }
        public string FORM_TYPE_CFG_CODE__EXACT { get; set; }

        public long? FORM_TYPE_CFG_ID { get; set; }

        public List<long> FORM_TYPE_CFG_IDs { get; set; }

        public bool? HAS_VALUE { get; set; }

        public HisFormTypeCfgDataViewFilter()
            : base()
        {
        }
    }
}
