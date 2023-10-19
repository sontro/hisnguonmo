using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.SDO
{
    public class AcsApplicationWithDataSDO : ACS_APPLICATION
    {
        public List<ACS_APP_OTP_TYPE> AppOtpTypes { get; set; }
    }
}
