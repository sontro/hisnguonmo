using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisWarningFeeCfg
{
    partial class HisWarningFeeCfgGet : EntityBase
    {
        public HIS_WARNING_FEE_CFG GetByCode(string code, HisWarningFeeCfgSO search)
        {
            HIS_WARNING_FEE_CFG result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_WARNING_FEE_CFG.AsQueryable().Where(p => p.WARNING_FEE_CFG_CODE == code);
                        if (search.listHisWarningFeeCfgExpression != null && search.listHisWarningFeeCfgExpression.Count > 0)
                        {
                            foreach (var item in search.listHisWarningFeeCfgExpression)
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
