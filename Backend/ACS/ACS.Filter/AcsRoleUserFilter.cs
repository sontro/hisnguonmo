using System;
using System.Collections.Generic;
using System.Text;

namespace ACS.Filter
{
    public class AcsRoleUserFilter : FilterBase
    {
        public long? USER_ID { get; set; }
        public long? ROLE_ID { get; set; }
        public AcsRoleUserFilter()
            : base()
        {
        }
    }
}
