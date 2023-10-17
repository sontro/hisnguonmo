using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqMety
{
    partial class HisServiceReqMetyGet : EntityBase
    {
        public HIS_SERVICE_REQ_METY GetByCode(string code, HisServiceReqMetySO search)
        {
            HIS_SERVICE_REQ_METY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SERVICE_REQ_METY.AsQueryable().Where(p => p.SERVICE_REQ_METY_CODE == code);
                        if (search.listHisServiceReqMetyExpression != null && search.listHisServiceReqMetyExpression.Count > 0)
                        {
                            foreach (var item in search.listHisServiceReqMetyExpression)
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
