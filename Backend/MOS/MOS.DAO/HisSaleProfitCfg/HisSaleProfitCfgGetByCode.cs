using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSaleProfitCfg
{
    partial class HisSaleProfitCfgGet : EntityBase
    {
        public HIS_SALE_PROFIT_CFG GetByCode(string code, HisSaleProfitCfgSO search)
        {
            HIS_SALE_PROFIT_CFG result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_SALE_PROFIT_CFG.AsQueryable().Where(p => p.SALE_PROFIT_CFG_CODE == code);
                        if (search.listHisSaleProfitCfgExpression != null && search.listHisSaleProfitCfgExpression.Count > 0)
                        {
                            foreach (var item in search.listHisSaleProfitCfgExpression)
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
