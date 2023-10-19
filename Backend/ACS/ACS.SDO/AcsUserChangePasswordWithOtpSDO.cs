using System;

namespace ACS.SDO
{
    public class AcsUserChangePasswordWithOtpSDO
    {
        public string Loginname { get; set; }
        public string Mobile { get; set; }
        public string Otp { get; set; }
        public string NewPassword { get; set; }
    }
}
