
namespace SDA.Filter
{
    public class SdaCommuneMapFilter : FilterBase
    {
        public string PARTNER_CODE__EXACT { get; set; }
        public string PARTNER_PROVINCE_CODE__EXACT { get; set; }
        public string PARTNER_DISTRICT_CODE__EXACT { get; set; }
        public string PARTNER_COMMUNE_CODE__EXACT { get; set; }
        public string COMMUNE_CODE__EXACT { get; set; }

        public SdaCommuneMapFilter()
            : base()
        {
        }
    }
}
