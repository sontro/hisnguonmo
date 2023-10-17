
namespace MOS.Filter
{
    public class HisTransReqFilter : FilterBase
    {
        public string TRANS_REQ_CODE__EXACT { get; set; }
        public string TIG_TRANSACTION_CODE__EXACT { get; set; }
        public long? TREATMENT_ID { get; set; }

        public bool? IS_CANCEL { get; set; }
        public HisTransReqFilter()
            : base()
        {
        }
    }
}
