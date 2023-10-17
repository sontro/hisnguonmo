using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodGet : EntityBase
    {
        public V_HIS_MEST_PERIOD_BLOOD GetViewByCode(string code, HisMestPeriodBloodSO search)
        {
            V_HIS_MEST_PERIOD_BLOOD result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_MEST_PERIOD_BLOOD.AsQueryable().Where(p => p.MEST_PERIOD_BLOOD_CODE == code);
                        if (search.listVHisMestPeriodBloodExpression != null && search.listVHisMestPeriodBloodExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisMestPeriodBloodExpression)
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
