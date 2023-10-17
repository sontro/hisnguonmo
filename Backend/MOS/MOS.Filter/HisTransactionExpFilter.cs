
namespace MOS.Filter
{
    public class HisTransactionExpFilter : FilterBase
    {
        public string EXP_MEST_CODE__EXACT { get; set; }
        public long? TRANSACTION_ID { get; set; }

        public HisTransactionExpFilter()
            : base()
        {
        }
    }
}
