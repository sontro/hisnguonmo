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
        internal List<V_HIS_DEBATE_REASON> GetView(HisDebateReasonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDebateReasonDAO.GetView(filter.Query(), param);
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
