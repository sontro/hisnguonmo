using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBedLog
{
    partial class HisBedLogGet : BusinessBase
    {
        internal List<V_HIS_BED_LOG_1> GetView1(HisBedLogView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBedLogDAO.GetView1(filter.Query(), param);
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
