using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockPeriod.ADO
{
    public class HisMestPeriodMediAdo : V_HIS_MEST_PERIOD_MEDI
    {
        public decimal KK_AMOUNT { get; set; }

        public HisMestPeriodMediAdo() { }
        public HisMestPeriodMediAdo(V_HIS_MEST_PERIOD_MEDI data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<HisMestPeriodMediAdo>(this, data);
            }
        }
    }
}
