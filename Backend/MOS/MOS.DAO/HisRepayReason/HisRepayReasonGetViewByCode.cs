using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRepayReason
{
    partial class HisRepayReasonGet : EntityBase
    {
        public V_HIS_REPAY_REASON GetViewByCode(string code, HisRepayReasonSO search)
        {
            V_HIS_REPAY_REASON result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_REPAY_REASON.AsQueryable().Where(p => p.REPAY_REASON_CODE == code);
                        if (search.listVHisRepayReasonExpression != null && search.listVHisRepayReasonExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisRepayReasonExpression)
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
