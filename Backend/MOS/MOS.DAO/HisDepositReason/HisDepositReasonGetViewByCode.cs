using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDepositReason
{
    partial class HisDepositReasonGet : EntityBase
    {
        public V_HIS_DEPOSIT_REASON GetViewByCode(string code, HisDepositReasonSO search)
        {
            V_HIS_DEPOSIT_REASON result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_DEPOSIT_REASON.AsQueryable().Where(p => p.DEPOSIT_REASON_CODE == code);
                        if (search.listVHisDepositReasonExpression != null && search.listVHisDepositReasonExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisDepositReasonExpression)
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
