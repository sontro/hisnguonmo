using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisSuggestedPayment
{
    public class ImpMestADO : V_HIS_IMP_MEST
    {
        public bool IsCheck { get; set; }

        public ImpMestADO() { }

        public ImpMestADO(V_HIS_IMP_MEST data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<ImpMestADO>(this, data);
                    if (data.IMP_MEST_PROPOSE_ID > 0)
                        IsCheck = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
