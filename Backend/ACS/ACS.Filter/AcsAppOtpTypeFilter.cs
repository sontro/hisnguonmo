
namespace ACS.Filter
{
    public class AcsAppOtpTypeFilter : FilterBase
    {
        public long? APPLICSTION_ID { get; set; }
        public long? APPLICATION_ID { get; set; }
        public long? OTP_TYPE_ID { get; set; }
        public AcsAppOtpTypeFilter()
            : base()
        {
        }
    }
}
