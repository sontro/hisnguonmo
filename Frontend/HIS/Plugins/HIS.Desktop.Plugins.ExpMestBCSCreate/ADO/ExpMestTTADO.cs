using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestBCSCreate.ADO
{
    class ExpMestTTADO : V_HIS_EXP_MEST_5
    {
        public bool IsCheck { get; set; }

        public ExpMestTTADO() { }

        public ExpMestTTADO(V_HIS_EXP_MEST_5 data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ExpMestTTADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
