using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Desktop.Plugins.AcsRoleUser
{
    public class RoleUserADO : ACS_ROLE_USER
    {
        public string LOGIN_NAME { get; set; }
        public string USER_NAME { get; set; }
        public Boolean IsBase { get; set; }
    }
}
