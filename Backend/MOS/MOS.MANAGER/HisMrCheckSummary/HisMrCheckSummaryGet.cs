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
        internal HisMrCheckSummaryGet()
            : base()
        {

        }

        internal HisMrCheckSummaryGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_MR_CHECK_SUMMARY> Get(HisMrCheckSummaryFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckSummaryDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECK_SUMMARY GetById(long id)
        {
            try
            {
                return GetById(id, new HisMrCheckSummaryFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECK_SUMMARY GetById(long id, HisMrCheckSummaryFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckSummaryDAO.GetById(id, filter.Query());
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
