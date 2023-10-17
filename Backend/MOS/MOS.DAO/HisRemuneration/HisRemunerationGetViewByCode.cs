using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRemuneration
{
    partial class HisRemunerationGet : EntityBase
    {
        public V_HIS_REMUNERATION GetViewByCode(string code, HisRemunerationSO search)
        {
            V_HIS_REMUNERATION result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_REMUNERATION.AsQueryable().Where(p => p.REMUNERATION_CODE == code);
                        if (search.listVHisRemunerationExpression != null && search.listVHisRemunerationExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisRemunerationExpression)
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
