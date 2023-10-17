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
        public HIS_DEBT_GOODS GetByCode(string code, HisDebtGoodsSO search)
        {
            HIS_DEBT_GOODS result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_DEBT_GOODS.AsQueryable().Where(p => p.DEBT_GOODS_CODE == code);
                        if (search.listHisDebtGoodsExpression != null && search.listHisDebtGoodsExpression.Count > 0)
                        {
                            foreach (var item in search.listHisDebtGoodsExpression)
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
