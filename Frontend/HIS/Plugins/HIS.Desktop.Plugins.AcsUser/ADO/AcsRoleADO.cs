using ACS.EFMODEL.DataModels;
using ACS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AcsUser.ADO
{
  class AcsRoleADO : V_ACS_ROLE_USER
  {
    public bool CHECKACSROLEUSER { get; set; }

    public AcsRoleADO() { }

    public AcsRoleADO(AcsRoleUserSDO data)
    {
      try
      {
        if (data != null)
        {
          Inventec.Common.Mapper.DataObjectMapper.Map<AcsRoleADO>(this,data);          
        }
      }
      catch (Exception ex)
      {
        Inventec.Common.Logging.LogSystem.Warn(ex);
      }
    }
  }
}
