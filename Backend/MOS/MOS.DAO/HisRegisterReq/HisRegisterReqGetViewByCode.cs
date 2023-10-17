using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRegisterReq
{
    partial class HisRegisterReqGet : EntityBase
    {
        public V_HIS_REGISTER_REQ GetViewByCode(string code, HisRegisterReqSO search)
        {
            V_HIS_REGISTER_REQ result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_REGISTER_REQ.AsQueryable().Where(p => p.REGISTER_REQ_CODE == code);
                        if (search.listVHisRegisterReqExpression != null && search.listVHisRegisterReqExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisRegisterReqExpression)
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
