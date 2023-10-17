
using System.Collections.Generic;
namespace SDA.Filter
{
    public class SdaConfigAppFilter : FilterBase
    {
        public string KEY { get; set; }
        public string APP_CODE { get; set; }
        public string APP_CODE_ACCEPT { get; set; }
        public List<string> MODULE_LINKSs { get; set; }

        public SdaConfigAppFilter()
            : base()
        {
        }
    }
}
