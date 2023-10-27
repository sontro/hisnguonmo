using ACS.EFMODEL.DataModels;
using System;

namespace ACS.SDO
{
    public class OtpRequiredWithMessageSDO
    {
        public OtpRequiredWithMessageSDO() { }

        public string LoginName { get; set; }
        public string Mobile { get; set; }
        public string Message_Format { get; set; }
        public string ApplicationCode { get; set; }
    }
}
