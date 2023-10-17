
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisPrepareMatyViewFilter : FilterBase
    {
        public string REQ_LOGINNAME__EXACT { get; set; }
        public string MATERIAL_TYPE_CODE__EXACT { get; set; }
        public string SERVICE_UNIT_CODE__EXACT { get; set; }
        public string MANUFACTURER_CODE__EXACT { get; set; }

        public long? PREPARE_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }
        public long? MANUFACTURER_ID { get; set; }
        public long? TDL_SERVICE_UNIT_ID { get; set; }

        public List<long> PREPARE_IDs { get; set; }
        public List<long> MATERIAL_TYPE_IDs { get; set; }
        public List<long> TDL_TREATMENT_IDs { get; set; }
        public List<long> MANUFACTURER_IDs { get; set; }
        public List<long> TDL_SERVICE_UNIT_IDs { get; set; }

        public HisPrepareMatyViewFilter()
            : base()
        {
        }
    }
}
