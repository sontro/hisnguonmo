using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodGet : BusinessBase
    {
        internal List<V_HIS_MEST_PERIOD_BLOOD> GetView(HisMestPeriodBloodViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestPeriodBloodDAO.GetView(filter.Query(), param);
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
