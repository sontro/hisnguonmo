using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathGet : EntityBase
    {
        public HIS_EVENTS_CAUSES_DEATH GetByCode(string code, HisEventsCausesDeathSO search)
        {
            HIS_EVENTS_CAUSES_DEATH result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_EVENTS_CAUSES_DEATH.AsQueryable().Where(p => p.EVENTS_CAUSES_DEATH_CODE == code);
                        if (search.listHisEventsCausesDeathExpression != null && search.listHisEventsCausesDeathExpression.Count > 0)
                        {
                            foreach (var item in search.listHisEventsCausesDeathExpression)
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
