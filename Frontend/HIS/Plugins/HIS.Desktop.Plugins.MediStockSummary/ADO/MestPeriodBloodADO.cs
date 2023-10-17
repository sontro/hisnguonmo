using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockSummary.ADO
{
	internal class MestPeriodBloodADO : MOS.EFMODEL.DataModels.HIS_MEST_PERIOD_BLOOD
	{
		public long BLOOD_TYPE_ID { get; set; }
		public long? EXPIRED_DATE { get; set; }

        public MestPeriodBloodADO(MOS.EFMODEL.DataModels.HIS_MEST_PERIOD_BLOOD data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<MediStockADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
