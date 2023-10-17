using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockPeriod.ADO
{
    public class HisMestPeriodMateAdo : V_HIS_MEST_PERIOD_MATE
    {
        public decimal KK_AMOUNT { get; set; }

        public HisMestPeriodMateAdo() { }
        public HisMestPeriodMateAdo(V_HIS_MEST_PERIOD_MATE data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<HisMestPeriodMateAdo>(this, data);
            }
        }
    }
}
