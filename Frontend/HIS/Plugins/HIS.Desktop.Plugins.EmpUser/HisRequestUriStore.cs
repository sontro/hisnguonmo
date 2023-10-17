using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EmpUser
{
    class HisRequestUriStore
    {
        internal const string HIS_EMPLOYEE_GET_VIEW = "api/HisEmployee/GetView";
        internal const string HIS_EMPLOYEE_DELETE = "api/HisEmployee/Delete";
        internal const string HIS_EMPLOYEE_CREATE = "api/HisEmployee/Create";
        internal const string HIS_EMPLOYEE_UPDATE = "api/HisEmployee/Update";
        internal const string HIS_EMPLOYEE_CHANGELOCK = "api/HisEmployee/ChangeLock";
        internal const string HIS_EMPLOYEE_GET = "api/HisEmployee/Get";
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
