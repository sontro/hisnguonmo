using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ComplexEventKey : Attribute
    {
        public const string APPLICATION_ROLE_DATA = "APPLICATION_ROLE_DATA";
        public const string MODULE_ROLE_DATA = "MODULE_ROLE_DATA";
        public const string ROLE_DATA = "ROLE_DATA";
        public const string ROLE_BASE_DATA = "ROLE_BASE_DATA";
        public const string ROLE_LIST_DATA = "ROLE_LIST_DATA";

        public string Value { get; set; }

        public ComplexEventKey(string value)
        {
            this.Value = value;
        }
    }
}
