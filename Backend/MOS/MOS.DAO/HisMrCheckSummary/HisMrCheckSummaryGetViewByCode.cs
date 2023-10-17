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
        public V_HIS_MR_CHECK_SUMMARY GetViewByCode(string code, HisMrCheckSummarySO search)
        {
            V_HIS_MR_CHECK_SUMMARY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MR_CHECK_SUMMARY.AsQueryable().Where(p => p.MR_CHECK_SUMMARY_CODE == code);
                        if (search.listVHisMrCheckSummaryExpression != null && search.listVHisMrCheckSummaryExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisMrCheckSummaryExpression)
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
