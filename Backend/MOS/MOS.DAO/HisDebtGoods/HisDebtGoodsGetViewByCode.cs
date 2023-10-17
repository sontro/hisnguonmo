using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDebtGoods
{
    partial class HisDebtGoodsGet : EntityBase
    {
        public V_HIS_DEBT_GOODS GetViewByCode(string code, HisDebtGoodsSO search)
        {
            V_HIS_DEBT_GOODS result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.V_HIS_DEBT_GOODS.AsQueryable().Where(p => p.DEBT_GOODS_CODE == code);
                        if (search.listVHisDebtGoodsExpression != null && search.listVHisDebtGoodsExpression.Count > 0)
                        {
                            foreach (var item in search.listVHisDebtGoodsExpression)
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
