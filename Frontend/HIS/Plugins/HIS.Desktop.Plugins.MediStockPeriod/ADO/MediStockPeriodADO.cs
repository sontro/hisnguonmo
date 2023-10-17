using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockPeriod.ADO
{
    public class MediStockPeriodADO : V_HIS_MEDI_STOCK_PERIOD
    {
        public bool IsCheck { get; set; }

        public MediStockPeriodADO() { }
        public MediStockPeriodADO(V_HIS_MEDI_STOCK_PERIOD data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<MediStockPeriodADO>(this, data);
            }
        }
    }
}
