using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SimpleEventKey : Attribute
    {
        public const string APPLICATION_CODE = "APPLICATION_CODE";
        public const string ROLE_CODE = "ROLE_CODE";
        public const string ROLE_BASE_CODE = "ROLE_BASE_CODE";
        public const string MODULE_LINK = "MODULE_LINK";

        public string Value { get; set; }

        public SimpleEventKey(string value)
        {
            this.Value = value;
        }
    } 
}
