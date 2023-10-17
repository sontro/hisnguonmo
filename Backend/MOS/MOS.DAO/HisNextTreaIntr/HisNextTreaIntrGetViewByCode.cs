using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNextTreaIntr
{
    partial class HisNextTreaIntrGet : EntityBase
    {
        public V_HIS_NEXT_TREA_INTR GetViewByCode(string code, HisNextTreaIntrSO search)
        {
            V_HIS_NEXT_TREA_INTR result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_NEXT_TREA_INTR.AsQueryable().Where(p => p.NEXT_TREA_INTR_CODE == code);
                        if (search.listVHisNextTreaIntrExpression != null && search.listVHisNextTreaIntrExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisNextTreaIntrExpression)
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
