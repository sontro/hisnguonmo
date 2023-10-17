
namespace MOS.Filter
{
    public class HisObeyContraindiFilter : FilterBase
    {
        public long? TREATMENT_ID { get; set; }
        public long? INTRUCTION_TIME { get; set; }
        public long? INTRUCTION_TIME_FROM { get; set; }
        public long? INTRUCTION_TIME_TO { get; set; }
        public long? REQUEST_DEPARTMENT_ID { get; set; }
        public string REQUEST_LOGINNAME__EXACT { get; set; }
        public string SERVICE_CODE__EXACT { get; set; }
        public string SERVICE_NAME__EXACT { get; set; }
        public string SERVICE_REQ_CODE__EXACT { get; set; }
        
        public HisObeyContraindiFilter()
            : base()
        {
        }
    }
}
