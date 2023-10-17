using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttGroupBest
{
    partial class HisPtttGroupBestGet : EntityBase
    {
        public HIS_PTTT_GROUP_BEST GetByCode(string code, HisPtttGroupBestSO search)
        {
            HIS_PTTT_GROUP_BEST result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_PTTT_GROUP_BEST.AsQueryable().Where(p => p.PTTT_GROUP_BEST_CODE == code);
                        if (search.listHisPtttGroupBestExpression != null && search.listHisPtttGroupBestExpression.Count > 0)
                        {
                            foreach (var item in search.listHisPtttGroupBestExpression)
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
