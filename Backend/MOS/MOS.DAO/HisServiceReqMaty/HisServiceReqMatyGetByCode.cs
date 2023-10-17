using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqMaty
{
    partial class HisServiceReqMatyGet : EntityBase
    {
        public HIS_SERVICE_REQ_MATY GetByCode(string code, HisServiceReqMatySO search)
        {
            HIS_SERVICE_REQ_MATY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SERVICE_REQ_MATY.AsQueryable().Where(p => p.SERVICE_REQ_MATY_CODE == code);
                        if (search.listHisServiceReqMatyExpression != null && search.listHisServiceReqMatyExpression.Count > 0)
                        {
                            foreach (var item in search.listHisServiceReqMatyExpression)
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
