using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlty
{
    partial class HisMestPeriodBltyGet : BusinessBase
    {
        internal List<V_HIS_MEST_PERIOD_BLTY> GetView(HisMestPeriodBltyViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestPeriodBltyDAO.GetView(filter.Query(), param);
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
