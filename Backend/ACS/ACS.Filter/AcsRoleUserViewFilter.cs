using System;
using System.Collections.Generic;
using System.Text;

namespace ACS.Filter
{
    public class AcsRoleUserViewFilter : FilterBase
    {
        public long? USER_ID { get; set; }
        public long? ROLE_ID { get; set; }
        public string LOGINNAME { get; set; }
        public string ROLE_CODE { get; set; }
        public AcsRoleUserViewFilter()
            : base()
        {
        }
    }
}
