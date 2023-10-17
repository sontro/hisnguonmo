using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSeseTransReq
{
    partial class HisSeseTransReqGet : EntityBase
    {
        public HIS_SESE_TRANS_REQ GetByCode(string code, HisSeseTransReqSO search)
        {
            HIS_SESE_TRANS_REQ result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SESE_TRANS_REQ.AsQueryable().Where(p => p.SESE_TRANS_REQ_CODE == code);
                        if (search.listHisSeseTransReqExpression != null && search.listHisSeseTransReqExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSeseTransReqExpression)
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
