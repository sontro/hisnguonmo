
namespace SDA.Filter
{
    public class SdaConfigAppUserViewFilter : FilterBase
    {
        public string KEY__EXACT { get; set; }
        public string LOGINNAME_EXACT { get; set; }

        public SdaConfigAppUserViewFilter()
            : base()
        {
        }
    }
}
