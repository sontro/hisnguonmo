using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepositReason
{
    partial class HisDepositReasonGet : BusinessBase
    {
        internal List<V_HIS_DEPOSIT_REASON> GetView(HisDepositReasonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDepositReasonDAO.GetView(filter.Query(), param);
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
