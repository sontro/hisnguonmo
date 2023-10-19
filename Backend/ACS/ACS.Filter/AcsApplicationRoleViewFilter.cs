using System;
using System.Collections.Generic;
using System.Text;

namespace ACS.Filter
{
    public class AcsApplicationRoleViewFilter : FilterBase
    {
        public string KEY_WORD { get; set; }
        public long? APPLICATION_ID { get; set; }
        public long? ROLE_ID { get; set; }

        public AcsApplicationRoleViewFilter()
            : base()
        {
        }
    }
}
