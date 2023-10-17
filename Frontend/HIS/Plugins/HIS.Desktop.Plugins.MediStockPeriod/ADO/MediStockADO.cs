using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockPeriod.ADO
{
    public class MediStockADO : V_HIS_MEDI_STOCK
    {
        public bool? IsOutUser { get; set; }

        public MediStockADO() { }
        public MediStockADO(V_HIS_MEDI_STOCK data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<MediStockADO>(this, data);
            }
        }
    }
}
