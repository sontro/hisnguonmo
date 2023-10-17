using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodMety
{
    partial class HisMestPeriodMetyGet : EntityBase
    {
        public HIS_MEST_PERIOD_METY GetByCode(string code, HisMestPeriodMetySO search)
        {
            HIS_MEST_PERIOD_METY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_MEST_PERIOD_METY.AsQueryable().Where(p => p.MEST_PERIOD_METY_CODE == code);
                        if (search.listHisMestPeriodMetyExpression != null && search.listHisMestPeriodMetyExpression.Count > 0)
                        {
                            foreach (var item in search.listHisMestPeriodMetyExpression)
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
