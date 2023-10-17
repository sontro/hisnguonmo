using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHealthExamRank
{
    partial class HisHealthExamRankGet : EntityBase
    {
        public HIS_HEALTH_EXAM_RANK GetByCode(string code, HisHealthExamRankSO search)
        {
            HIS_HEALTH_EXAM_RANK result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_HEALTH_EXAM_RANK.AsQueryable().Where(p => p.HEALTH_EXAM_RANK_CODE == code);
                        if (search.listHisHealthExamRankExpression != null && search.listHisHealthExamRankExpression.Count > 0)
                        {
                            foreach (var item in search.listHisHealthExamRankExpression)
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
