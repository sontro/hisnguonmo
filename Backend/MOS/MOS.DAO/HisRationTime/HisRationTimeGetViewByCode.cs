using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationTime
{
    partial class HisRationTimeGet : EntityBase
    {
        public V_HIS_RATION_TIME GetViewByCode(string code, HisRationTimeSO search)
        {
            V_HIS_RATION_TIME result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_RATION_TIME.AsQueryable().Where(p => p.RATION_TIME_CODE == code);
                        if (search.listVHisRationTimeExpression != null && search.listVHisRationTimeExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisRationTimeExpression)
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
