using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceFollow
{
    partial class HisServiceFollowGet : EntityBase
    {
        public V_HIS_SERVICE_FOLLOW GetViewByCode(string code, HisServiceFollowSO search)
        {
            V_HIS_SERVICE_FOLLOW result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERVICE_FOLLOW.AsQueryable().Where(p => p.SERVICE_FOLLOW_CODE == code);
                        if (search.listVHisServiceFollowExpression != null && search.listVHisServiceFollowExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisServiceFollowExpression)
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
