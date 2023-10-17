using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentHurt
{
    partial class HisAccidentHurtGet : EntityBase
    {
        public V_HIS_ACCIDENT_HURT GetViewByCode(string code, HisAccidentHurtSO search)
        {
            V_HIS_ACCIDENT_HURT result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ACCIDENT_HURT.AsQueryable().Where(p => p.ACCIDENT_HURT_CODE == code);
                        if (search.listVHisAccidentHurtExpression != null && search.listVHisAccidentHurtExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAccidentHurtExpression)
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
