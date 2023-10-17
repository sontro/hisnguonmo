using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.ADO
{
    public class ExpMestMedicineADO : V_HIS_EXP_MEST_MEDICINE
    {

        public decimal YCT_AMOUNT { get; set; }
        public bool IsMoba { get; set; }
        public bool IsError { get; set; }

        public decimal? EXP_PRICE { get; set; }
        public decimal? EXP_VAT_RATIO { get; set; }

        public ExpMestMedicineADO() { }

        public ExpMestMedicineADO(V_HIS_EXP_MEST_MEDICINE data)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestMedicineADO>(this, data);
                this.EXP_PRICE = data.PRICE;
                this.EXP_VAT_RATIO = data.VAT_RATIO;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
