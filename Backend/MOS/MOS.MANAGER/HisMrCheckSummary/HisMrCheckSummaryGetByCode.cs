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
        internal HIS_MR_CHECK_SUMMARY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMrCheckSummaryFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECK_SUMMARY GetByCode(string code, HisMrCheckSummaryFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckSummaryDAO.GetByCode(code, filter.Query());
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
