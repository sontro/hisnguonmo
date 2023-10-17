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
        internal V_HIS_MR_CHECK_SUMMARY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMrCheckSummaryViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MR_CHECK_SUMMARY GetViewByCode(string code, HisMrCheckSummaryViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckSummaryDAO.GetViewByCode(code, filter.Query());
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
