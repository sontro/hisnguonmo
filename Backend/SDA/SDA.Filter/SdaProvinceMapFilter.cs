
namespace SDA.Filter
{
    public class SdaProvinceMapFilter : FilterBase
    {
        public string PARTNER_CODE__EXACT { get; set; }
        public string PARTNER_PROVINCE_CODE__EXACT { get; set; }
        public string PROVINCE_CODE__EXACT { get; set; }

        public SdaProvinceMapFilter()
            : base()
        {
        }
    }
}
