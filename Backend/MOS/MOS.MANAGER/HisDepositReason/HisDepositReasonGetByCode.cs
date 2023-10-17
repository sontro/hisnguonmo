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
        internal HIS_DEPOSIT_REASON GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDepositReasonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEPOSIT_REASON GetByCode(string code, HisDepositReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDepositReasonDAO.GetByCode(code, filter.Query());
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
