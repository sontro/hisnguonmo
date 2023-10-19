using System;

namespace ACS.SDO
{
    public class AcsUserChangePasswordSDO
    {
        public string LOGIN_NAME { get; set; }
        public string PASSWORD__OLD { get; set; }
        public string PASSWORD__NEW { get; set; }
    }
}
