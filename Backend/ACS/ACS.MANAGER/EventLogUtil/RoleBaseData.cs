using ACS.LibraryEventLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    class RoleBaseData
    {
        public string RoleCode { get; set; }
        public string RoleName { get; set; }

        public List<string> RoleBaseCodes { get; set; }

        public RoleBaseData()
        {
        }

        public RoleBaseData(string roleCode, string roleName, List<string> roleBaseCodes)
        {
            this.RoleCode = roleCode;
            this.RoleName = roleName;
            this.RoleBaseCodes = roleBaseCodes;
        }

        public override string ToString()
        {
            string children = this.RoleBaseCodes != null ?
                string.Join(",", this.RoleBaseCodes) : "";

            string vaitro = LogCommonUtil.GetEventLogContent(EventLog.Enum.VaiTro);
            string vaitrokethua = LogCommonUtil.GetEventLogContent(EventLog.Enum.VaiTroKeThua);
            string aggrExpCode = string.IsNullOrWhiteSpace(this.RoleCode) ?
                "" : string.Format("{0}: {1} - {2}", vaitro, this.RoleCode, this.RoleName);
            return string.Format("{0} ({1}: {2})", aggrExpCode, vaitrokethua, children);
        }
    }
}
