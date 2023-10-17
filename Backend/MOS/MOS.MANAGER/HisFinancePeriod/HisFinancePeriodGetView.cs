using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFinancePeriod
{
    partial class HisFinancePeriodGet : BusinessBase
    {
        internal List<V_HIS_FINANCE_PERIOD> GetView(HisFinancePeriodViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisFinancePeriodDAO.GetView(filter.Query(), param);
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
