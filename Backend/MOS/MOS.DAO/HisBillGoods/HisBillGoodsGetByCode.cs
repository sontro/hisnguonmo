using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBillGoods
{
    partial class HisBillGoodsGet : EntityBase
    {
        public HIS_BILL_GOODS GetByCode(string code, HisBillGoodsSO search)
        {
            HIS_BILL_GOODS result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNullOrEmpty(code);
                if (valid)
                {
                    using (var ctx = new AppContext())
                    {
                        var query = ctx.HIS_BILL_GOODS.AsQueryable().Where(p => p.BILL_GOODS_CODE == code);
                        if (search.listHisBillGoodsExpression != null && search.listHisBillGoodsExpression.Count > 0)
                        {
                            foreach (var item in search.listHisBillGoodsExpression)
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
