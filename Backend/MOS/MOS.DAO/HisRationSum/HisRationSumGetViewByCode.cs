using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRationSum
{
    partial class HisRationSumGet : EntityBase
    {
        public V_HIS_RATION_SUM GetViewByCode(string code, HisRationSumSO search)
        {
            V_HIS_RATION_SUM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_RATION_SUM.AsQueryable().Where(p => p.RATION_SUM_CODE == code);
                        if (search.listVHisRationSumExpression != null && search.listVHisRationSumExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisRationSumExpression)
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
