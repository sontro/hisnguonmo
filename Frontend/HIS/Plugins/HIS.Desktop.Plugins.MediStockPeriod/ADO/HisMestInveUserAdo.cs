using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockPeriod.ADO
{
    public class HisMestInveUserAdo : HIS_MEST_INVE_USER
    {
        public int Action { get; set; }

        public HisMestInveUserAdo() { }
        public HisMestInveUserAdo(HIS_MEST_INVE_USER data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<HisMestInveUserAdo>(this, data);
            }
        }
    }
}
