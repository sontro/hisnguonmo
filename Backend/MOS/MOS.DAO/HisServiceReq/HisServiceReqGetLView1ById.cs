using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReq
{
    partial class HisServiceReqGet : EntityBase
    {
        public L_HIS_SERVICE_REQ_1 GetLView1ById(long id, HisServiceReqSO search)
        {
            L_HIS_SERVICE_REQ_1 result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.L_HIS_SERVICE_REQ_1.AsQueryable().Where(p => p.ID == id);
                        if (search.listLHisServiceReq1Expression != null && search.listLHisServiceReq1Expression.Count > 0)
                        {
                            foreach (var item in search.listLHisServiceReq1Expression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
