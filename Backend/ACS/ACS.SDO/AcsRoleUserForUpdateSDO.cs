using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace ACS.SDO
{
    public class AcsRoleUserForUpdateSDO
    {
        public ACS_USER User { get; set; }
        public List<ACS_ROLE_USER> RoleUsers { get; set; }
    }
}
