using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisRoleUser.ADO
{
    public class RoleUserAdo : HIS_IMP_MEST_USER
    {
        public int Action { get; set; }

        public RoleUserAdo() { }
        public RoleUserAdo(HIS_IMP_MEST_USER data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<RoleUserAdo>(this, data);
            }
        }
    }
}
