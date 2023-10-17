using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrCheckSummary
{
    partial class HisMrCheckSummaryGet : EntityBase
    {
        public HIS_MR_CHECK_SUMMARY GetByCode(string code, HisMrCheckSummarySO search)
        {
            HIS_MR_CHECK_SUMMARY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MR_CHECK_SUMMARY.AsQueryable().Where(p => p.MR_CHECK_SUMMARY_CODE == code);
                        if (search.listHisMrCheckSummaryExpression != null && search.listHisMrCheckSummaryExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMrCheckSummaryExpression)
                            {
                                query = query.Where(item);
                            }
                        }
                        result = query.SingleOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => code), code) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
