using System.Collections.Generic;
namespace SAR.Filter
{
    public class SarPrintTypeCfgFilter : FilterBase
    {
        public string BRANCH_CODE__EXACT { get; set; }
        public string APP_CODE__EXACT { get; set; }
        public string MODULE_LINK__EXACT { get; set; }
        public string CONTROL_CODE__EXACT { get; set; }
        public string CONTROL_PATH__EXACT { get; set; }
        public long? PRINT_TYPE_ID { get; set; }
        public List<long> PRINT_TYPE_IDs { get; set; }

        public SarPrintTypeCfgFilter()
            : base()
        {
        }
    }
}
