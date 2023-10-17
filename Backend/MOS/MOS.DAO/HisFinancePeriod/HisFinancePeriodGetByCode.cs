using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisFinancePeriod
{
    partial class HisFinancePeriodGet : EntityBase
    {
        public HIS_FINANCE_PERIOD GetByCode(string code, HisFinancePeriodSO search)
        {
            HIS_FINANCE_PERIOD result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_FINANCE_PERIOD.AsQueryable().Where(p => p.FINANCE_PERIOD_CODE == code);
                        if (search.listHisFinancePeriodExpression != null && search.listHisFinancePeriodExpression.Count > 0)
                        {
                            foreach (var item in search.listHisFinancePeriodExpression)
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
