using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.SDO
{
    public class AuthenRequestTDO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AuthenticationCode { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
