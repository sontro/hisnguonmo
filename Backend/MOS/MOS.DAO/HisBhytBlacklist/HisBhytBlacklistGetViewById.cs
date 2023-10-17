using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytBlacklist
{
    partial class HisBhytBlacklistGet : EntityBase
    {
        public V_HIS_BHYT_BLACKLIST GetViewById(long id, HisBhytBlacklistSO search)
        {
            V_HIS_BHYT_BLACKLIST result = null;
            try
            {
                bool valid = true;
                valid = valid && IsGreaterThanZero(id);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_BHYT_BLACKLIST.AsQueryable().Where(p => p.ID == id);
                        if (search.listVHisBhytBlacklistExpression != null && search.listVHisBhytBlacklistExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisBhytBlacklistExpression)
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
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search), LogType.Error);
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
