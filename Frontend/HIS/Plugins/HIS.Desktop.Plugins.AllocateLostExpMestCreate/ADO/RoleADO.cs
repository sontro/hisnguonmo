using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocateLostExpMestCreate.ADO
{
    public class RoleADO : HIS_EXP_MEST_USER
    {
        public bool Action { get; set; }

        public RoleADO()
        {
        }
        public RoleADO(HIS_EXP_MEST_USER expMestUser)
        {
            if (expMestUser != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<RoleADO>(this, expMestUser);
            }
        }
    }
}
