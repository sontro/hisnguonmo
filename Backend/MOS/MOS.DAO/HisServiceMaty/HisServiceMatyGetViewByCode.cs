using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceMaty
{
    partial class HisServiceMatyGet : EntityBase
    {
        public V_HIS_SERVICE_MATY GetViewByCode(string code, HisServiceMatySO search)
        {
            V_HIS_SERVICE_MATY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_SERVICE_MATY.AsQueryable().Where(p => p.SERVICE_MATY_CODE == code);
                        if (search.listVHisServiceMatyExpression != null && search.listVHisServiceMatyExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisServiceMatyExpression)
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
