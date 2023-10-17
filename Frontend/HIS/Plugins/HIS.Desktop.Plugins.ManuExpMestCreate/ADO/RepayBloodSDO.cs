using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ManuExpMestCreate.ADO
{
    public class RepayBloodSDO : MOS.SDO.HisImpMestBloodWithInStockInfoSDO
    {
        public bool IsCheck { get; set; }

        public RepayBloodSDO() { }

        public RepayBloodSDO(HisImpMestBloodWithInStockInfoSDO data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<RepayBloodSDO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
