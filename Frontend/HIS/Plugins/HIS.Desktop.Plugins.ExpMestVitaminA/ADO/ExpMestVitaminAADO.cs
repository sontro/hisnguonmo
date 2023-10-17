using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestVitaminA.ADO
{
    public class ExpMestVitaminAADO : V_HIS_VITAMIN_A
    {
        public decimal AVAILABLE { get; set; }
        public decimal TOTAL_IN_MEDI_STOCK { get; set; }

        public ExpMestVitaminAADO()
        {
        }

        public ExpMestVitaminAADO(V_HIS_VITAMIN_A data)
        {

            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestVitaminAADO>(this, data);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
			
        }

        public ExpMestVitaminAADO(HisMedicineInStockSDO data)
        {
        }

        //public ExpMestVitaminAADO(V_HIS_EXP_MEST_MEDICINE data)
        //{
        //    try
        //    {
        //        if (data != null)
        //        {
        //            Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestVitaminAADO>(this, data);
        //            this.MEDICINE_TYPE_CODE = data.MEDICINE_TYPE_CODE;
        //            this.MEDICINE_TYPE_NAME = data.MEDICINE_TYPE_NAME;
        //            this.VIR_TOTAL_IMP_PRICE = this.AMOUNT * (this.PRICE ?? 0) * (1 + (this.VAT_RATIO ?? 0));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //    }
        //}
    }
}
