using ACS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    class ApplicationRoleData
    {
        public string ApplicationCode { get; set; }
        public string ApplicationName { get; set; }

        public List<string> RoleNames { get; set; }

        public ApplicationRoleData()
        {
        }

        public ApplicationRoleData(string applicationCode, string applicationName, List<string> roleNames)
        {
            this.ApplicationCode = applicationCode;
            this.ApplicationName = applicationName;
            this.RoleNames = roleNames;
        }

        public ApplicationRoleData(string applicationCode, List<string> roleNames)
        {
            this.ApplicationCode = applicationCode;
            this.RoleNames = roleNames;
        }

        public override string ToString()
        {
            string roleName = RoleNames != null ?
                string.Join(",", RoleNames) : "";
            string srCode = string.IsNullOrWhiteSpace(this.ApplicationCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.APPLICATION_CODE, this.ApplicationCode);
            string vaitro = LogCommonUtil.GetEventLogContent(EventLog.Enum.VaiTro);
            return string.Format("{0} ({1}: {2})", srCode, vaitro, roleName);
        }
    }
}
