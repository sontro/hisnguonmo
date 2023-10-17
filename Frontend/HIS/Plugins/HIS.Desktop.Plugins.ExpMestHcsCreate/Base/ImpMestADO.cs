using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestHcsCreate.Base
{
    public class ImpMestADO : V_HIS_IMP_MEST
    {
        public bool XBTT_Or_XHTT { get; set; }
        public bool Check { get; set; }

        public ImpMestADO() { }

        public ImpMestADO(V_HIS_IMP_MEST data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ImpMestADO>(this, data);
                this.XBTT_Or_XHTT = (data.XBTT_EXP_MEST_ID != null || data.XHTT_EXP_MEST_ID != null) ? false : true;
            }
        }
    }
}
