using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceChangeReq
{
    partial class HisServiceChangeReqGet : EntityBase
    {
        public V_HIS_SERVICE_CHANGE_REQ GetViewByCode(string code, HisServiceChangeReqSO search)
        {
            V_HIS_SERVICE_CHANGE_REQ result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERVICE_CHANGE_REQ.AsQueryable().Where(p => p.SERVICE_CHANGE_REQ_CODE == code);
                        if (search.listVHisServiceChangeReqExpression != null && search.listVHisServiceChangeReqExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisServiceChangeReqExpression)
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
