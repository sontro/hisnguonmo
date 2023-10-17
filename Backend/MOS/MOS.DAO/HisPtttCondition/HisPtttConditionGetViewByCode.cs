using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttCondition
{
    partial class HisPtttConditionGet : EntityBase
    {
        public V_HIS_PTTT_CONDITION GetViewByCode(string code, HisPtttConditionSO search)
        {
            V_HIS_PTTT_CONDITION result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_PTTT_CONDITION.AsQueryable().Where(p => p.PTTT_CONDITION_CODE == code);
                        if (search.listVHisPtttConditionExpression != null && search.listVHisPtttConditionExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisPtttConditionExpression)
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
