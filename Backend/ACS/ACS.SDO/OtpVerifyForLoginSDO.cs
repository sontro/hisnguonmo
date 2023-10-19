using ACS.EFMODEL.DataModels;
using System;

namespace ACS.SDO
{
    public class OtpVerifyForLoginSDO
    {
        public OtpVerifyForLoginSDO() { }

        public string LoginAddress { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string ApplicationCode { get; set; }
        public string Otp { get; set; }
    }
}
