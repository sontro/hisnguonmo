using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.Base
{
    class SmsResponseCode
    {
        internal const string SUCCESS = "00";
        internal const string ERROR = "99";

        internal const string AUTHENTICATE_FAIL = "10";
        internal const string MOBILE_INVALID = "11";
        internal const string CONTENT_EMPTY = "12";
        internal const string CONTENT_MAXLENGHT = "13";
    }
}
