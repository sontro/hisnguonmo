using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServPttt
{
    partial class HisSereServPtttGet : BusinessBase
    {
        internal List<V_HIS_SERE_SERV_PTTT_1> GetView1(HisSereServPtttView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServPtttDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
