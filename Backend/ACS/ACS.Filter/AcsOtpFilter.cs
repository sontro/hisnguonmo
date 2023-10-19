
using System.Collections.Generic;
namespace ACS.Filter
{
    public class AcsOtpFilter : FilterBase
    {
        public string OTP_CODE__EXACT { get; set; }
        public short? OTP_TYPE { get; set; }
        public string LOGINNAME__EXACT { get; set; }
        public string MOBILE__EXACT { get; set; }
        public long? OTP_TYPE_ID { get; set; }

        public bool? IS_HAS_EXPIRE { get; set; }
        public long? EXPIRE_DATE__EXACT { get; set; }

        public string LOGINNAME_OR_MOBILE__EXACT { get; set; }

        public List<short> OTP_TYPEs { get; set; }
        public List<long> OTP_TYPE_IDs { get; set; }

        public AcsOtpFilter()
            : base()
        {
        }
    }
}
