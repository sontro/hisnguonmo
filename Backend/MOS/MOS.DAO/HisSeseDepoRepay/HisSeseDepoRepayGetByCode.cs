using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayGet : EntityBase
    {
        public HIS_SESE_DEPO_REPAY GetByCode(string code, HisSeseDepoRepaySO search)
        {
            HIS_SESE_DEPO_REPAY result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SESE_DEPO_REPAY.AsQueryable().Where(p => p.SESE_DEPO_REPAY_CODE == code);
                        if (search.listHisSeseDepoRepayExpression != null && search.listHisSeseDepoRepayExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSeseDepoRepayExpression)
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
