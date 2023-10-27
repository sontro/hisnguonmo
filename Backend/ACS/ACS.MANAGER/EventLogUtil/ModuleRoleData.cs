using ACS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    class ModuleRoleData
    {
        public string RoleCode { get; set; }
        public string RoleName { get; set; }

        public List<string> ModuleLinks { get; set; }

        public ModuleRoleData()
        {
        }

        public ModuleRoleData(string roleCode, string roleName, List<string> moduleLinks)
        {
            this.RoleCode = roleCode;
            this.RoleName = roleName;
            this.ModuleLinks = moduleLinks;
        }

        public override string ToString()
        {
            string children = this.ModuleLinks != null ?
                string.Join(",", this.ModuleLinks) : "";

            string chucnang = LogCommonUtil.GetEventLogContent(EventLog.Enum.ChucNang);
            string aggrImpCode = string.IsNullOrWhiteSpace(this.RoleCode) ?
                "" : string.Format("{0}: {1} - {2}", SimpleEventKey.ROLE_CODE, this.RoleCode, this.RoleName);
            return string.Format("{0} ({1}: {2})", aggrImpCode, chucnang, children);
        }
    }
}
