
namespace MOS.Filter
{
    public class HisServiceChangeReqViewFilter : FilterBase
    {
        public string SERVICE_REQ_CODE__EXACT { get; set; }
        public string TDL_TREATMENT_CODE__EXACT { get; set; }
        public long? TDL_SERVICE_REQ_ID { get; set; }
        public long? ALTER_SERVICE_ID { get; set; }
        public long? EXECUTE_ROOM_ID { get; set; }
        public long? REQUEST_ROOM_ID { get; set; }
        public bool? HAS_APPROVAL_LOGINNAME { get; set; }
        public bool? HAS_APPROVAL_CASHIER_LOGINNAME { get; set; }

        public HisServiceChangeReqViewFilter()
            : base()
        {
        }
    }
}
