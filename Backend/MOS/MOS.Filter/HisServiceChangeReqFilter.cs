
namespace MOS.Filter
{
    public class HisServiceChangeReqFilter : FilterBase
    {
        public long? TDL_SERVICE_REQ_ID { get; set; }
        public long? ALTER_SERVICE_ID { get; set; }
        public bool? HAS_APPROVAL_LOGINNAME { get; set; }
        public bool? HAS_APPROVAL_CASHIER_LOGINNAME { get; set; }
        public long? SERE_SERV_ID { get; set; }

        public HisServiceChangeReqFilter()
            : base()
        {
        }
    }
}
