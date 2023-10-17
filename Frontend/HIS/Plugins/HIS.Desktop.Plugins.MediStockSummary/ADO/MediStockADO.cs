using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockSummary.ADO
{
    public class MediStockADO : V_HIS_MEDI_STOCK
    {
        public bool IsCheck { get; set; }
        public long TYPE { get; set; }

        public MediStockADO() { }

        public MediStockADO(V_HIS_MEDI_STOCK data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<MediStockADO>(this, data);
                    this.TYPE = 999;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
