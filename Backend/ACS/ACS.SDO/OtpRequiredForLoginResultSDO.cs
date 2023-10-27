using ACS.EFMODEL.DataModels;
using System;

namespace ACS.SDO
{
    public class OtpRequiredForLoginResultSDO
    {
        public OtpRequiredForLoginResultSDO() { }

        public string LoginName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
    }
}
