
namespace SDA.Filter
{
    public class SdaCustomizeButtonFilter : FilterBase
    {
        public string APP_CODE__EXACT { get; set; }
        public string MODULE_LINK__EXACT { get; set; }
        public string BRANCH_CODE__EXACT { get; set; }

        public SdaCustomizeButtonFilter()
            : base()
        {
        }
    }
}
