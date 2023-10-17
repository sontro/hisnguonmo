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
        public V_HIS_SERVICE_REQ_MATY GetViewById(long id, HisServiceReqMatySO search)
        {
            V_HIS_SERVICE_REQ_MATY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERVICE_REQ_MATY.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisServiceReqMatyExpression != null && search.listVHisServiceReqMatyExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisServiceReqMatyExpression)
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
