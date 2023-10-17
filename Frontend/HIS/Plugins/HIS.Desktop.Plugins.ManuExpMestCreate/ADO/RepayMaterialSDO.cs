using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ManuExpMestCreate.ADO
{
    public class RepayMaterialSDO : MOS.SDO.HisImpMestMaterialWithInStockAmountSDO
    {
        public decimal RepayAmountInput { get; set; }
        public bool IsCheck { get; set; }
        public decimal AmountOld { get; set; }

        public RepayMaterialSDO() { }

        public RepayMaterialSDO(HisImpMestMaterialWithInStockAmountSDO data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<RepayMedicineSDO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
