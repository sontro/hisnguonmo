
namespace MOS.Filter
{
    public class HisExecuteRoleFilter : FilterBase
    {
        public bool? IS_SURG_MAIN { get; set; }
        public bool? HAS_ALLOW_SIMULTANEITY { get; set; }

        public HisExecuteRoleFilter()
            : base()
        {
        }
    }
}
