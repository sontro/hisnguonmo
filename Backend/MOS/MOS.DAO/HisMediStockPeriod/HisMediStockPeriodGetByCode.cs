using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMediStockPeriod
{
    partial class HisMediStockPeriodGet : EntityBase
    {
        public HIS_MEDI_STOCK_PERIOD GetByCode(string code, HisMediStockPeriodSO search)
        {
            HIS_MEDI_STOCK_PERIOD result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEDI_STOCK_PERIOD.AsQueryable().Where(p => p.MEDI_STOCK_PERIOD_CODE == code);
                        if (search.listHisMediStockPeriodExpression != null && search.listHisMediStockPeriodExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMediStockPeriodExpression)
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
