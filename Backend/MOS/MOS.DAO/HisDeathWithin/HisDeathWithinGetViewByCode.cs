using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDeathWithin
{
    partial class HisDeathWithinGet : EntityBase
    {
        public V_HIS_DEATH_WITHIN GetViewByCode(string code, HisDeathWithinSO search)
        {
            V_HIS_DEATH_WITHIN result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_DEATH_WITHIN.AsQueryable().Where(p => p.DEATH_WITHIN_CODE == code);
                        if (search.listVHisDeathWithinExpression != null && search.listVHisDeathWithinExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisDeathWithinExpression)
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
