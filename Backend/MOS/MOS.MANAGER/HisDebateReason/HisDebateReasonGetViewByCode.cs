using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateReason
{
    partial class HisDebateReasonGet : BusinessBase
    {
        internal V_HIS_DEBATE_REASON GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisDebateReasonViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DEBATE_REASON GetViewByCode(string code, HisDebateReasonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateReasonDAO.GetViewByCode(code, filter.Query());
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
