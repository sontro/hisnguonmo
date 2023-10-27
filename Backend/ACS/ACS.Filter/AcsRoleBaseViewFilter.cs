using System;
using System.Collections.Generic;
using System.Text;

namespace ACS.Filter
{
    public class AcsRoleBaseViewFilter : FilterBase
    {
        public long? ROLE_ID { get; set; }
        public AcsRoleBaseViewFilter()
            : base()
        {
        }
    }
}
