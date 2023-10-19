using Inventec.Common.Logging;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    class RoleData
    {
        public string RoleCode { get; set; }
        public string RoleName { get; set; }     

        public RoleData()
        {
        }

        public RoleData(string roleCode, string roleName)
        {
            this.RoleCode = roleCode;
            this.RoleName = roleName;
        }

        public override string ToString()
        {
            string roleCode = RoleCode != null ? RoleCode : "";
            string roleName = RoleName != null ? RoleName : "";

            return string.Format("{0}: {1} ({2})", SimpleEventKey.ROLE_CODE, roleCode, roleName);
        }
    }
}
