using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ApiConsumer
{
    public class AcsRequestUriStore
    {
        public const string ACS_MODULE_GROUP_GET = "api/AcsModuleGroup/Get";
        public const string ACS_USER_GET = "api/AcsUser/Get";
        public const string ACS_TOKEN__AUTHORIZE = "api/AcsToken/Authorize";
        public const string ACS_TIMER__SYNC = "api/Timer/Sync";
        public const string ACS_TOKEN__GETCREDENTIALTRACKING = "api/Token/GetCredentialTracking";


        public static string ACS_TOKEN__GETCREDENTIALTRACKING_USER { get; set; }
    }
}
