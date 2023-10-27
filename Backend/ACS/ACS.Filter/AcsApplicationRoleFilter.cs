using System;
using System.Collections.Generic;
using System.Text;

namespace ACS.Filter
{
    public class AcsApplicationRoleFilter : FilterBase
    {
        public long? APPLICATION_ID { get; set; }
        public long? ROLE_ID { get; set; }

        public AcsApplicationRoleFilter()
            : base()
        {
        }
    }
}
