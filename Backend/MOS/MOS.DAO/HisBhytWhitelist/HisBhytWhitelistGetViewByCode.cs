using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytWhitelist
{
    partial class HisBhytWhitelistGet : EntityBase
    {
        public V_HIS_BHYT_WHITELIST GetViewByCode(string code, HisBhytWhitelistSO search)
        {
            V_HIS_BHYT_WHITELIST result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_BHYT_WHITELIST.AsQueryable().Where(p => p.BHYT_WHITELIST_CODE == code);
                        if (search.listVHisBhytWhitelistExpression != null && search.listVHisBhytWhitelistExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisBhytWhitelistExpression)
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
