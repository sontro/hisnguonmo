
namespace SDA.Filter
{
    public class SdaDistrictMapFilter : FilterBase
    {
        public string PARTNER_CODE__EXACT { get; set; }
        public string PARTNER_PROVINCE_CODE__EXACT { get; set; }
        public string PARTNER_DISTRICT_CODE__EXACT { get; set; }
        public string DISTRICT_CODE__EXACT { get; set; }

        public SdaDistrictMapFilter()
            : base()
        {
        }
    }
}
