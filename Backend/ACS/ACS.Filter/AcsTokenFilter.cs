
using System.Collections.Generic;
namespace ACS.Filter
{
    public class AcsTokenFilter : FilterBase
    {
        public string ApplicationCode { get; set; }
        public string TokenCode { get; set; }
        public string RenewCode { get; set; }
        public List<string> TokenCodes { get; set; }
        public string MachineName { get; set; }
        public string LoginName { get; set; }
        public List<string> LoginNames { get; set; }

        public AcsTokenFilter()
            : base()
        {
        }
    }
}
