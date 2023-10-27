using Inventec.Common.Logging;
using ACS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.MANAGER.EventLogUtil
{
    class RoleListData
    {
        public string ApplicationCode { get; set; }
        public List<ACS_ROLE> RoleDatas { get; set; }

        public RoleListData()
        {
        }

        public RoleListData(string applicationCode, List<ACS_ROLE> roleDatas)
        {
            this.ApplicationCode = applicationCode;
            this.RoleDatas = roleDatas;
        }

        public override string ToString()
        {
            string children = this.RoleDatas != null ?
              string.Join(",", RoleDatas.Select(o => o.ROLE_CODE + " - " + o.ROLE_NAME).ToList()) : "";

            string aggrImpCode = string.IsNullOrWhiteSpace(this.ApplicationCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.APPLICATION_CODE, this.ApplicationCode);
            return string.Format("{0} ({1}: {2})", aggrImpCode, SimpleEventKey.ROLE_CODE, children);
        }
    }
}
