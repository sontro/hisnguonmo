using ACS.EFMODEL.DataModels;
using System;

namespace ACS.SDO
{
    public class OtpRequiredForLoginSDO
    {
        public OtpRequiredForLoginSDO() { }

        public string LoginName { get; set; }
        public string Password { get; set; }
        public string ApplicationCode { get; set; }
    }
}
