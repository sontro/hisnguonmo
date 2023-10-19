using System;

namespace ACS.SDO
{
    public class LoginByEmailTDO
    {
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string APPLICATION_CODE { get; set; }
        public string APP_VERSION { get; set; }
        public string LOGIN_ADDRESS { get; set; }
    }
}
