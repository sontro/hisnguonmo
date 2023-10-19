using System;
using System.Collections.Generic;
using System.Text;

namespace ACS.Filter
{
    public class AcsRoleBaseFilter : FilterBase
    {
        public long? ROLE_ID { get; set; }
        public long? ROLE_BASE_ID { get; set; }
        public AcsRoleBaseFilter()
            : base()
        {
        }
    }
}
