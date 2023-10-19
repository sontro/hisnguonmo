
namespace ACS.Filter
{
    public class AcsAuthenRequestFilter : FilterBase
    {
        public string AUTHENTICATION_CODE { get; set; }
        public string AUTHOR_SYSTEM_CODE { get; set; }
        public bool? IS_ISSUED_TOKEN { get; set; }

        public AcsAuthenRequestFilter()
            : base()
        {
        }
    }
}
