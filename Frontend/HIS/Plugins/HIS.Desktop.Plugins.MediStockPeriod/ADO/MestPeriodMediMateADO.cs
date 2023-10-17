using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockPeriod.ADO
{
    public class MestPeriodMediMateADO : MOS.EFMODEL.DataModels.V_HIS_MEST_PERIOD_MEDI
    {
        public string METY_MATY_CODE { get; set; }
        public string METY_MATY_NAME { get; set; }
        public string IS_MEDICINE { get; set; }
        public string AMOUNT_STR { get; set; }
        //public decimal AMOUNT { get; set; }
        public string EXPIRED_DATE_STR { get; set; }

        public MestPeriodMediMateADO()
        {
        }

        public MestPeriodMediMateADO(MOS.EFMODEL.DataModels.V_HIS_MEST_PERIOD_MEDI item)
        {
            try
            {
                if (item != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<MestPeriodMediMateADO>(this, item);
                    this.METY_MATY_CODE = item.MEDICINE_TYPE_CODE;
                    this.METY_MATY_NAME = item.MEDICINE_TYPE_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }

        public MestPeriodMediMateADO(MOS.EFMODEL.DataModels.V_HIS_MEST_PERIOD_MATE item)
        {
            try
            {
                if (item != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<MestPeriodMediMateADO>(this, item);
                    this.METY_MATY_CODE = item.MATERIAL_TYPE_CODE;
                    this.METY_MATY_NAME = item.MATERIAL_TYPE_NAME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
