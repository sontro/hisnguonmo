using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AcsUser
{
    public class HisRequestUriStore
    {
      internal const string ACS_USER_GET_VIEW = "api/AcsUser/GetView";
      internal const string ACS_USER_DELETE = "api/AcsUser/Delete";
      internal const string ACS_USER_CREATE = "api/AcsUser/Create";
      internal const string ACS_USER_UPDATE = "api/AcsUser/Update";
      internal const string ACS_USER_RESET = "api/AcsUser/ResetPassword";
      internal const string ACS_USER_CHANGELOCK = "api/AcsUser/ChangeLock";
      internal const string ACS_USER_GET = "api/AcsUser/Get";
      internal const string SDA_GROUP_GET = "api/SdaGroup/Get";
      internal const string ACS_ROLE_GET = "api/AcsRole/Get";
      internal const string ACS_ROLE_USER_GET = "api/AcsRoleUser/Get";
      internal const string ACS_ROLE_USER_GET_VIEW = "api/AcsRoleUser/GetForTree";
      internal const string ACS_ROLE_USER_UPDATE = "api/AcsRoleUser/UpdateWithRole";      
    }
}
