using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAccidentPoison
{
    partial class HisAccidentPoisonGet : EntityBase
    {
        public V_HIS_ACCIDENT_POISON GetViewByCode(string code, HisAccidentPoisonSO search)
        {
            V_HIS_ACCIDENT_POISON result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_ACCIDENT_POISON.AsQueryable().Where(p => p.ACCIDENT_POISON_CODE == code);
                        if (search.listVHisAccidentPoisonExpression != null && search.listVHisAccidentPoisonExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisAccidentPoisonExpression)
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
