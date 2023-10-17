using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AcsUser.ADO
{
  public class AcsUserADO : SDA_GROUP
  {
    public bool CHECKEDIT { get; set; }
    public AcsUserADO()
    {
    }
    public AcsUserADO(SDA_GROUP data)
    {
      if (data != null)
      {
        //Inventec.Common.Mapper.
        Inventec.Common.Mapper.DataObjectMapper.Map<AcsUserADO>(this, data);
      }
    }
  }
}
