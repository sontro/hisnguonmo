
namespace SDA.Filter
{
    public class SdaLicenseFilter : FilterBase
    {
        public long Time { get; set; }
        public string JsonMessage { get; set; }

        public string APP_CODE { get; set; }
        public string APP_CODE__EXACT { get; set; }
        public string CLIENT_CODE__EXACT { get; set; }

        public bool? IS_EXPIRED { get; set; }

        public SdaLicenseFilter()
            : base()
        {
        }
    }
}
