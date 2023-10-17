using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEkipPlan
{
    partial class HisEkipPlanGet : EntityBase
    {
        public HIS_EKIP_PLAN GetByCode(string code, HisEkipPlanSO search)
        {
            HIS_EKIP_PLAN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EKIP_PLAN.AsQueryable().Where(p => p.EKIP_PLAN_CODE == code);
                        if (search.listHisEkipPlanExpression != null && search.listHisEkipPlanExpression.Count > 0)
                        {
                            foreach (var item in search.listHisEkipPlanExpression)
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
