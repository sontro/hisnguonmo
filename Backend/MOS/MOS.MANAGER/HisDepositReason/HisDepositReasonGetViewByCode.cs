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
        internal V_HIS_DEPOSIT_REASON GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisDepositReasonViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DEPOSIT_REASON GetViewByCode(string code, HisDepositReasonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDepositReasonDAO.GetViewByCode(code, filter.Query());
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
