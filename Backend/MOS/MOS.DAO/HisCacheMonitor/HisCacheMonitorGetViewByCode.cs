using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCacheMonitor
{
    partial class HisCacheMonitorGet : EntityBase
    {
        public V_HIS_CACHE_MONITOR GetViewByCode(string code, HisCacheMonitorSO search)
        {
            V_HIS_CACHE_MONITOR result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_CACHE_MONITOR.AsQueryable().Where(p => p.CACHE_MONITOR_CODE == code);
                        if (search.listVHisCacheMonitorExpression != null && search.listVHisCacheMonitorExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisCacheMonitorExpression)
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