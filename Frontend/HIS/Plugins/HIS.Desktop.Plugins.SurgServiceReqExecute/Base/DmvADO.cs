using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
    public class DmvADO : HIS_STENT_CONCLUDE
    {
        public int Action { get; set; }
        public DmvADO()
        {
        }
        public DmvADO(HIS_STENT_CONCLUDE data)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<DmvADO>(this, data);
        }
    }
}
