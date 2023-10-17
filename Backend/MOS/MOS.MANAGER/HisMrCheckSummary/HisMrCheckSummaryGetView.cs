using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckSummary
{
    partial class HisMrCheckSummaryGet : BusinessBase
    {
        internal List<V_HIS_MR_CHECK_SUMMARY> GetView(HisMrCheckSummaryViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckSummaryDAO.GetView(filter.Query(), param);
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
