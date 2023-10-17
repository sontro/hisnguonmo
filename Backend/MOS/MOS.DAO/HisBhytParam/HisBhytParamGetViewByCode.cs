using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytParam
{
    partial class HisBhytParamGet : EntityBase
    {
        public V_HIS_BHYT_PARAM GetViewByCode(string code, HisBhytParamSO search)
        {
            V_HIS_BHYT_PARAM result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_BHYT_PARAM.AsQueryable().Where(p => p.BHYT_PARAM_CODE == code);
                        if (search.listVHisBhytParamExpression != null && search.listVHisBhytParamExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisBhytParamExpression)
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
