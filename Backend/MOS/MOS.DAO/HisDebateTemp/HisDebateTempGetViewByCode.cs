using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebateTemp
{
    partial class HisDebateTempGet : EntityBase
    {
        public V_HIS_DEBATE_TEMP GetViewByCode(string code, HisDebateTempSO search)
        {
            V_HIS_DEBATE_TEMP result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_DEBATE_TEMP.AsQueryable().Where(p => p.DEBATE_TEMP_CODE == code);
                        if (search.listVHisDebateTempExpression != null && search.listVHisDebateTempExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisDebateTempExpression)
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