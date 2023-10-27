using System;

namespace ACS.SDO
{
    public class LoginByAuthenRequestTDO
    {
        public string LOGIN_ADDRESS { get; set; }
        public string AuthenticationCode { get; set; }
        public string AuthorSystemCode { get; set; }
        public string AppCode { get; set; }
    }
}
