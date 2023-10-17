using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialUseCount
{
    public class DataADO : V_HIS_EXP_MEST_MATERIAL
    {
        public string MEDI_STOCK_NAME { get; set; }

        public string REUSE_COUNT_STR { get; set; }

        public string VAT_RATIO_STR { get; set; }

        public long ReusCount { get; set; }

        public DataADO() { }

        public DataADO(V_HIS_EXP_MEST_MATERIAL data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<DataADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
