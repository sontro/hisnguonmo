using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEkipPlanUser
{
    partial class HisEkipPlanUserGet : EntityBase
    {
        public V_HIS_EKIP_PLAN_USER GetViewByCode(string code, HisEkipPlanUserSO search)
        {
            V_HIS_EKIP_PLAN_USER result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_EKIP_PLAN_USER.AsQueryable().Where(p => p.EKIP_PLAN_USER_CODE == code);
                        if (search.listVHisEkipPlanUserExpression != null && search.listVHisEkipPlanUserExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisEkipPlanUserExpression)
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
