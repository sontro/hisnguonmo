
using System.Collections.Generic;
namespace ACS.Filter
{
    public class AcsCredentialDataFilter : FilterBase
    {
        public string RESOURCE_SYSTEM_CODE { get; set; }
        public string TOKEN_CODE { get; set; }
        public List<string> TOKEN_CODEs { get; set; }
        public List<string> NOT_IN_TOKEN_CODEs { get; set; }
        public string DATA_KEY { get; set; }

        public AcsCredentialDataFilter()
            : base()
        {
        }
    }
}
