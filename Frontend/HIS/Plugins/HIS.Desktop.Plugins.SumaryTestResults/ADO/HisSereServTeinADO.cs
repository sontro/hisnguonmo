using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SumaryTestResults.ADO
{
    public class HisSereServTeinADO : MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_TEIN
    {
        public HisSereServTeinADO()
        {

        }

        public HisSereServTeinADO(MOS.EFMODEL.DataModels.V_HIS_SERE_SERV_TEIN data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<HisSereServTeinADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public bool IsAdd { get; set; }
    }
}
