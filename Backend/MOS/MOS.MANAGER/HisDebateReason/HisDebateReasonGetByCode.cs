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
        internal HIS_DEBATE_REASON GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisDebateReasonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEBATE_REASON GetByCode(string code, HisDebateReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateReasonDAO.GetByCode(code, filter.Query());
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
