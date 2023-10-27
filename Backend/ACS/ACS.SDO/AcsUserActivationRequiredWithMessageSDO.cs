using System;

namespace ACS.SDO
{
    public class AcsUserActivationRequiredWithMessageSDO
    {
        public string LOGINNAME { get; set; }
        public string MOBILE { get; set; }
        public string APPLICATIONCODE { get; set; } 
        public string MESSAGE_FORMAT { get; set; }
    }
}
